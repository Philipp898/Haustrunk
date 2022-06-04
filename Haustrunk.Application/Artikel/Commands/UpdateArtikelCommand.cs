using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Domain.Enums;
using MediatR;

namespace Haustrunk.Application.Artikel.Commands
{
    public record UpdateArtikelCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Marke { get; init; } = string.Empty;
        public string Sorte { get; init; } = string.Empty;
        public Gebinde Gebinde { get; init; }
    }

    public class UpdateArtikelCommandHandler : IRequestHandler<UpdateArtikelCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateArtikelCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(UpdateArtikelCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Artikel.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Artikel nicht gefunden mit der ID: {request.Id}");
            }

            entity.Marke = request.Marke;
            entity.Sorte = request.Sorte;
            entity.Gebinde = request.Gebinde;
            entity.LastModified = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
