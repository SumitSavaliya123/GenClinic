using GenClinic_Common.Constants;
using GenClinic_Common.Enums;
using GenClinic_Common.Exceptions;
using GenClinic_Common.Utils;
using Microsoft.AspNetCore.Authorization;

namespace GenClinic_Api.ExtAuthorization
{
    public class ExtAuthorizeHandler : AuthorizationHandler<ExtAuthorizeRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ExtAuthorizeHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExtAuthorizeRequirement requirement)
        {
            HttpContext? httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            new AuthHelper(httpContext, _configuration).AuthorizeRequest();

            if (CheckUserType(httpContext, requirement))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }

        private static bool CheckUserType(HttpContext context, ExtAuthorizeRequirement requirement)
        {
            LoggedUser? loggedUser = context.Items[SystemConstants.LOGGED_USER] as LoggedUser;

            if (loggedUser == null) return false;

            // Handle the Policy requirement
            if (requirement.PolicyName == SystemConstants.DoctorPolicy)
            {
                if (loggedUser.Role == UserRole.Doctor) return true;
            }
            else if (requirement.PolicyName == SystemConstants.PatientPolicy)
            {
                if (loggedUser.Role == UserRole.Patient) return true;
            }
            else if (requirement.PolicyName == SystemConstants.LabUserPolicy)
            {
                if (loggedUser.Role == UserRole.Lab) return true;
            }
            else if (requirement.PolicyName == SystemConstants.AllUserPolicy)
            {
                if (loggedUser.Role == UserRole.Lab || loggedUser.Role == UserRole.Doctor || loggedUser.Role == UserRole.Patient) return true;
            }

            throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.UNAUTHORIZE);
        }
    }
}