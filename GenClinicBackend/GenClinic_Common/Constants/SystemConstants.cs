namespace GenClinic_Common.Constants
{
    public class SystemConstants
    {
        public static readonly int PASSWORD_ITERATION = 10;
        public static readonly string UNAUTHORIZE = "Unauthorize access!";
        public static readonly string USER_ID_CLAIM = "UserID";
        public static readonly string LOGGED_USER = "LoggedUser";
        public static readonly string TOKEN_EXPIRE = "Token is expired!";
        public static readonly string INVALID_TOKEN = "Invalid Token!";


        #region Otp
        public const string AuthenticationOtp = "AuthenticationOtp";

        #endregion

        #region Policy Attribute

        public const string DoctorPolicy = "Doctor";

        public const string PatientPolicy = "Patient";

        public const string LabUserPolicy = "LabUser";

        public const string AllUserPolicy = "AllUser";

        #endregion Policy Attribute
    }
}