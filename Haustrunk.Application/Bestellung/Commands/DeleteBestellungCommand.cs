using Haustrunk.Application.Common.Interfaces;
using MediatR;

namespace Haustrunk.Application.Bestellung.Commands
{
    public record DeleteBestellungCommand :IRequest
    {
        public Guid Id { get; init; }
    }
    public class DeleteBestellungCommandHandler : IRequestHandler<DeleteBestellungCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteBestellungCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteBestellungCommand request, CancellationToken cancellationToken)
        {
            var bestellungEntity = await _context.Bestellungen.FindAsync(new object[] { request.Id },cancellationToken);

            if (bestellungEntity == null)
            {
                throw new KeyNotFoundException($"Bestellung nicht gefunden mit der ID: {request.Id}");
            }

            _context.Bestellungen.Remove(bestellungEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
