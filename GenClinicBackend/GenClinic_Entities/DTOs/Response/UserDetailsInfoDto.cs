using GenClinic_Common.Enums;

namespace GenClinic_Entities.DTOs.Response
{
    public class UserDetailsInfoDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTimeOffset? DOB { get; set; }
        public Gender? Gender { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Avatar { get; set; }
    }
}