using System.ComponentModel.DataAnnotations;
using GenClinic_Common.Enums;

namespace GenClinic_Entities.DTOs.Request
{
    public class RegisterUserRequestDto
    {
        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string RepeatPassword { get; set; } = null!;

        [Required]
        public DateTimeOffset? DateOfBirth { get; set; }

        [Required]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public Gender? Gender { get; set; }

        [Required]
        public string Address { get; set; } = string.Empty;

        public bool IsPatient { get; set; } = true;
    }
}