using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GenClinic_Common.Constants;
using GenClinic_Common.Enums;
using GenClinic_Common.Exceptions;
using GenClinic_Common.Utils;
using GenClinic_Common.Utils.Models;
using Microsoft.IdentityModel.Tokens;

namespace GenClinic_Api.ExtAuthorization
{
    public class AuthHelper
    {
        #region Properties
        public HttpContext _httpContext;
        public IConfiguration _config;
        #endregion Properties

        #region Constructor
        public AuthHelper(HttpContext httpContext, IConfiguration configuration)
        {
            _httpContext = httpContext;
            _config = configuration;
        }
        #endregion Constructor

        #region Method
        internal void AuthorizeRequest()
        {
            string jsonToken = _httpContext.Request.Headers.Authorization.FirstOrDefault() ?? throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.UNAUTHORIZE);

            JwtSetting jwtSetting = GetJwtSetting(_config);

            ClaimsPrincipal? claims = GetClaimsWithValidationToken(jwtSetting, jsonToken) ?? throw new UnauthorizedAccessException();

            // Create the CurrentUserModel object from the claims
            SetLoggedUser(_httpContext, claims);
        }
        #endregion Method

        #region Helper Method
        private static JwtSetting GetJwtSetting(IConfiguration configuration)
        {
            JwtSetting jwtSetting = new();
            configuration.GetSection("Jwt").Bind(jwtSetting);
            return jwtSetting;
        }

        private static void SetLoggedUser(HttpContext httpContext, ClaimsPrincipal claims)
        {
            LoggedUser loggedUser = new()
            {
                UserId = Convert.ToInt64(claims.FindFirstValue(SystemConstants.USER_ID_CLAIM) ?? string.Empty),
                Role = Enum.Parse<UserRole>(claims.FindFirstValue(ClaimTypes.Role) ?? string.Empty),
                Name = claims.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
                Email = claims.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            };

            // Set the authenticated user
            ClaimsIdentity? identity = new ClaimsIdentity(claims.Identity);
            ClaimsPrincipal? principal = new ClaimsPrincipal(identity);
            httpContext.User = principal;

            // Attach the CurrentUserModel to the HttpContext.User
            httpContext.Items[SystemConstants.LOGGED_USER] = loggedUser;
        }

        public ClaimsPrincipal? GetClaimsWithValidationToken(JwtSetting jwtSetting, string jsonToken)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            byte[] key = Encoding.ASCII.GetBytes(jwtSetting.Key);

            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidIssuer = jwtSetting.Issuer,
                ClockSkew = TimeSpan.Zero
            };

            if (!tokenHandler.CanReadToken(jsonToken))
                throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.INVALID_TOKEN);

            JwtSecurityToken? securityToken = tokenHandler.ReadJwtToken(jsonToken);

            if (securityToken.ValidTo < DateTime.UtcNow) throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.TOKEN_EXPIRE);

            ClaimsPrincipal? claims = tokenHandler.ValidateToken(jsonToken, validationParameters, out var validatedToken);
            IsTokenExpire(jsonToken);

            return claims;
        }

        public void IsTokenExpire(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            if (jsonToken.ValidTo < DateTime.UtcNow) throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.TOKEN_EXPIRE);
        }

        public LoggedUser GetLoggedUser()
        {
            string jsonToken = _httpContext.Request.Headers.Authorization.FirstOrDefault() ?? throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.UNAUTHORIZE);

            JwtSetting jwtSetting = GetJwtSetting(_config);

            ClaimsPrincipal? claims = GetClaimsWithValidationToken(jwtSetting, jsonToken);

            return new LoggedUser
            {
                UserId = Convert.ToInt64(claims?.FindFirstValue(SystemConstants.USER_ID_CLAIM) ?? string.Empty),
                Role = Enum.Parse<UserRole>(claims?.FindFirstValue(ClaimTypes.Role) ?? string.Empty),
                Name = claims?.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
                Email = claims?.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            };
        }
        #endregion Helper Method

    }
}