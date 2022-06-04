namespace Haustrunk.Application.Dtos
{
    public class BestellungDto
    {
        public BestellungDto()
        {
            Bestellpositionen = new List<BestellpositionDto>();
        }
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime BestelltZu { get; set; }
        public IList<BestellpositionDto> Bestellpositionen { get; set; }
    }
}
