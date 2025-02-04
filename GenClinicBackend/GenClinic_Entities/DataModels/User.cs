using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GenClinic_Common.Enums;
using GenClinic_Entities.Abstract;

namespace GenClinic_Entities.DataModels
{
    public class User : AuditableEntity<long>
    {
        [StringLength(16)]
        [Column("first_name", TypeName = "varchar")]
        public string FirstName { get; set; } = null!;

        [StringLength(16)]
        [Column("last_name", TypeName = "varchar")]
        public string LastName { get; set; } = null!;

        [StringLength(128)]
        [Column("email", TypeName = "varchar")]
        public string Email { get; set; } = null!;

        [MaxLength(255)]
        [Column("password", TypeName = "varchar")]
        public string Password { get; set; } = null!;

        [StringLength(13)]
        [Column("phone_number", TypeName = "varchar")]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(512)]
        [Column("address", TypeName = "varchar")]
        public string Address { get; set; } = null!;

        [Column("dob")]
        public DateTimeOffset? DOB { get; set; }

        [Column("role")]
        public UserRole Role { get; set; }

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("doctor_id")]
        public long? DoctorId { get; set; }

        [Column("lab_id")]
        public long? LabId { get; set; }

        [Column("avatar")]
        public byte[]? Avatar { get; set; }

        [MaxLength(6)]
        [Column("otp", TypeName = "varchar")]
        public string? OTP { get; set; }

        [Column("expiry_time")]
        public DateTimeOffset? ExpiryTime { get; set; }

        [ForeignKey(nameof(LabId))]
        public virtual User LabUsers { get; set; } = null!;

        [ForeignKey(nameof(DoctorId))]
        public virtual User DoctorUsers { get; set; } = null!;

    }
}