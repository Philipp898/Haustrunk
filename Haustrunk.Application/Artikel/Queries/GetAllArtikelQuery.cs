using EnumsNET;
using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Haustrunk.Application.Artikel.Queries
{
    public record GetAllArtikelQuery : IRequest<List<ArtikelDto>>;

    public class GetAllArtikelQueryHandler : IRequestHandler<GetAllArtikelQuery, List<ArtikelDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllArtikelQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ArtikelDto>> Handle(GetAllArtikelQuery request, CancellationToken cancellationToken)
        {
            var allEntities = await _context.Artikel.ToListAsync();

            if (allEntities == null)
            {
                throw new KeyNotFoundException($"Es konnten keine Artikel gefunden werden");
            }

            return allEntities.Select(art => new ArtikelDto()
            {
                Id = art.Id,
                Marke = art.Marke,
                Sorte = art.Sorte,
                Gebinde = art.Gebinde.AsString(EnumFormat.Description) ?? string.Empty,
            }).ToList();

        }
    }
}