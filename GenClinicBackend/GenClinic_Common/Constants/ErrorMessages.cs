namespace GenClinic_Common.Constants
{
    public class ErrorMessages
    {

        public const string USER_ALREADY_EXISTS = "User is already exists!";

        public const string USER_NOT_FOUND = "User is not exists!";

        public const string INVALID_USER = "Email or Password is incorrect!";

        public static class ExceptionMessage
        {
            public const string INTERNAL_SERVER = "An error occurred while processing the request";

            public const string INVALID_MODELSTATE = "Invalid Entry";
        }
    }
}