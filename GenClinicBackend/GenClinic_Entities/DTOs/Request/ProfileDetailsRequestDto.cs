using GenClinic_Common.Enums;
using Microsoft.AspNetCore.Http;

namespace GenClinic_Entities.DTOs.Request
{
    public class ProfileDetailsRequestDto
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? Headline { get; set; }

        public DateTimeOffset? DOB { get; set; }

        public Gender? Gender { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Address { get; set; } = null!;

        public IFormFile? Avatar { get; set; }
    }
}