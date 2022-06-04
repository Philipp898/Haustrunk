using Haustrunk.Domain.Enums;

namespace Haustrunk.Application.Dtos
{
    public class ArtikelDto
    {
        public Guid Id { get; set; }
        public string Marke { get; set; } = string.Empty;
        public string Sorte { get; set; } = string.Empty;
        public string Gebinde { get; set; } = string.Empty;
    }
}
