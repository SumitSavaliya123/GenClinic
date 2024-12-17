using System.ComponentModel.DataAnnotations;

namespace GenClinic_Entities.DTOs.Request
{
    public class LoginOtpRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;
        [Required]
        public string Otp { get; set; } = null!;
    }
}