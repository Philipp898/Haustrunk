namespace Haustrunk.Application.Dtos
{
    public class BestellpositionDto
    {
        public int Id { get; set; }
        public Guid ArtikelId { get; set; }
        public int Bestellmenge { get; set; }
    }
}
