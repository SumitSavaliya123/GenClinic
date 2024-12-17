using Microsoft.AspNetCore.Authorization;

namespace GenClinic_Api.ExtAuthorization
{
    public class ExtAuthorizeRequirement : IAuthorizationRequirement
    {
        public string PolicyName { get; }

        public ExtAuthorizeRequirement(string policyName)
        {
            PolicyName = policyName;
        }
    }
}