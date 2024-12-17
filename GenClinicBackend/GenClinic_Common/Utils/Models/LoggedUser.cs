using GenClinic_Common.Enums;

namespace GenClinic_Common.Utils
{
    public class LoggedUser
    {
        public long UserId { get; set; }
        public UserRole Role { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public long LabId { get; set; }
    }
}