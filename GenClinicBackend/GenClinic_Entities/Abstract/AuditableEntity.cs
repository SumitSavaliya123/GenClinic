using System.ComponentModel.DataAnnotations.Schema;
using GenClinic_Entities.DataModels;

namespace GenClinic_Entities.Abstract
{
    public class AuditableEntity<T> : IdentityEntity<T>
    {
        [Column("created_on")]
        public virtual DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        [Column("updated_on")]
        public virtual DateTimeOffset? UpdatedOn { get; set; }

        [Column("is_deleted")]
        public bool? IsDeleted { get; set; } = false;

        [Column("created_by")]
        public long? CreatedBy { get; set; }

        [Column("updated_by")]
        public long? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual User? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual User? UpdatedByUser { get; set; }
    }
}