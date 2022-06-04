using Haustrunk.Domain.Common;

namespace Haustrunk.Domain.Entities
{
    public class Bestellposition : AuditableEntity
    {
        public int Id { get; set; }
        public Guid ArtikelId { get; set; }
        public int Bestellmenge { get; set; }
        public Bestellung Bestellung { get; set; } = null!; 
    }
}
