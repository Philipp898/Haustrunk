using Haustrunk.Domain.Common;

namespace Haustrunk.Domain.Entities
{
    public class Bestellung : AuditableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime BestelltZu { get; set; }
        public List<Bestellposition> Bestellpositionen { get; set; } = new();
    }
}
