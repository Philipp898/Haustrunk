using EnumsNET;
using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Application.Dtos;
using MediatR;

namespace Haustrunk.Application.Artikel.Queries
{
    public record GetArtikelQuery : IRequest<ArtikelDto>
    {
        public Guid Id { get; init; }
    }

    public class GetArtikelQueryHandler : IRequestHandler<GetArtikelQuery, ArtikelDto>
    {
        private readonly IApplicationDbContext _context;

        public GetArtikelQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ArtikelDto> Handle(GetArtikelQuery request, CancellationToken cancellationToken)
        {
            var entity =  await _context.Artikel.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Artikel nicht gefunden mit der ID: {request.Id}");
            }

            return new ArtikelDto()
            {
                Id = entity.Id,
                Marke = entity.Marke,
                Sorte = entity.Sorte,
                Gebinde = entity.Gebinde.AsString(EnumFormat.Description) ?? string.Empty,
            };
        }
    }
}
