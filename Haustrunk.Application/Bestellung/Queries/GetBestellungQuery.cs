using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Application.Dtos;
using MediatR;

namespace Haustrunk.Application.Bestellung.Queries
{
    public record GetBestellungQuery : IRequest<BestellungDto>
    {
        public Guid Id { get; init; }
    }

    public class GetBestellungQueryHandler : IRequestHandler<GetBestellungQuery, BestellungDto>
    {
        private readonly IApplicationDbContext _context;

        public GetBestellungQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<BestellungDto> Handle(GetBestellungQuery request, CancellationToken cancellationToken)
        {
            var bestellungEntity = await _context.Bestellungen.FindAsync(new object[] { request.Id }, cancellationToken);

            if (bestellungEntity == null)
            {
                throw new KeyNotFoundException($"Bestellung nicht gefunden mit der ID: {request.Id}");
            }

            return new BestellungDto()
            {
                Id = bestellungEntity.Id,
                Bestellpositionen = bestellungEntity.Bestellpositionen.Select(bp => new BestellpositionDto() { ArtikelId = bp.ArtikelId, Id = bp.Id, Bestellmenge = bp.Bestellmenge }).ToList(),
                BestelltZu = bestellungEntity.BestelltZu,
                UserId = bestellungEntity.UserId,
            };
        }
    }
}
