namespace Haustrunk.Domain.Common
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedUtc { get; set; } = DateTime.Now;

        public string LastModifiedBy { get; set; } = string.Empty;

        public DateTime? LastModified { get; set; }
    }
}