using System.ComponentModel.DataAnnotations;

namespace GenClinic_Entities.DTOs.Request
{
    public class LoginEmailRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;
    }
}