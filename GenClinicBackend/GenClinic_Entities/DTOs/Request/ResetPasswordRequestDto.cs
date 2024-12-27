using System.ComponentModel.DataAnnotations;

namespace GenClinic_Entities.DTOs.Request
{
    public class ResetPasswordRequestDto
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{4,}$", ErrorMessage = "Invalid password format.")]
        public string password { get; set; } = null!;
    }
}