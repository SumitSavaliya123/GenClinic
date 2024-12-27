using GenClinic_Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace GenClinic_Api.Helpers
{
    public class DoctorPolicyAttribute : AuthorizeAttribute
    {
        public DoctorPolicyAttribute()
        {
            Policy = SystemConstants.DoctorPolicy;
        }
    }

    public class PatientPolicyAttribute : AuthorizeAttribute
    {
        public PatientPolicyAttribute()
        {
            Policy = SystemConstants.PatientPolicy;
        }
    }

    public class LabUserPolicyAttribute : AuthorizeAttribute
    {
        public LabUserPolicyAttribute()
        {
            Policy = SystemConstants.LabUserPolicy;
        }
    }
    public class AllPolicyAttribute : AuthorizeAttribute
    {
        public AllPolicyAttribute()
        {
            Policy = SystemConstants.AllUserPolicy;
        }
    }
}