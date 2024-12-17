using GenClinic_Common.Constants;

namespace GenClinic_Common.Utils.Models
{
    public class PasswordUtil
    {
        public static string HashPassword(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor: SystemConstants.PASSWORD_ITERATION);

            string passwordWithSalt = $"{hashedPassword}{salt}";

            return passwordWithSalt;
        }

        public static bool VerifyPassword(string password, string hashedPasswordWithSalt)
        {
            string hashedPassword = hashedPasswordWithSalt.Substring(0, 60);

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, hashedPassword);

            return isPasswordCorrect;
        }
    }
}