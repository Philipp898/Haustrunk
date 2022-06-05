using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Haustrunk.Application.Bestellung.Queries
{
    public record GetAllBestellungQuery : IRequest<List<BestellungDto>>;

    public class GetAllBestellungQueryHandler : IRequestHandler<GetAllBestellungQuery, List<BestellungDto>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllBestellungQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<BestellungDto>> Handle(GetAllBestellungQuery request, CancellationToken cancellationToken)
        {
            var allBestellungen = await _context.Bestellungen.ToListAsync();

            if (allBestellungen == null)
            {
                throw new KeyNotFoundException($"Es konnten keine Bestellungen gefunden werden");
            }

            return allBestellungen.Select(best => new BestellungDto()
            {
                BestelltZu = best.BestelltZu,
                Id = best.Id,
                UserId = best.UserId,
                Bestellpositionen = best.Bestellpositionen.Select(bp => new BestellpositionDto() { ArtikelId = bp.ArtikelId, Id = bp.Id, Bestellmenge = bp.Bestellmenge}).ToList()
            }).ToList();
        }
    }
}
