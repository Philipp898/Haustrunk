using Haustrunk.Application.Common.Interfaces;
using MediatR;

namespace Haustrunk.Application.Artikel.Commands
{
    public record DeleteArtikelCommand : IRequest
    {
        public Guid Id { get; init; }
    }
    public class DeleteArtikelCommandHandler : IRequestHandler<DeleteArtikelCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteArtikelCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteArtikelCommand request, CancellationToken cancellationToken)
        {
           var entity = await _context.Artikel.FindAsync(new object[] { request.Id },cancellationToken);

            if(entity == null)
            {
                throw new KeyNotFoundException($"Artikel nicht gefunden mit der ID: {request.Id}");
            }
            _context.Artikel.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
