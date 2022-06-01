using Haustrunk.Domain.Common;
using Haustrunk.Domain.Enums;

namespace Haustrunk.Domain.Entities
{
    public class Artikel : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Marke { get; set; } = string.Empty;
        public string Sorte { get; set; } = string.Empty;
        public Gebinde Gebinde { get ; set;}
    }
}
