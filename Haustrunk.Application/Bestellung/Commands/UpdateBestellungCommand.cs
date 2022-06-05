using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haustrunk.Application.Bestellung.Commands
{
    public record UpdateBestellungCommand :IRequest
    {
        public Guid Id { get; init; }
        public DateTime BestelltZu { get; init; }
        public IList<Bestellposition> Bestellpositionen { get; init; } = new List<Bestellposition>();
    }

    public class UpdateBestellungCommandHandler : IRequestHandler<UpdateBestellungCommand>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _userService;
        public UpdateBestellungCommandHandler(IDateTimeService dateTime, IApplicationDbContext context, IIdentityService identityService, ICurrentUserService userService)
        {
            _dateTime = dateTime;
            _context = context;
            _identityService = identityService;
            _userService = userService;
        }

        public async Task<Unit> Handle(UpdateBestellungCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserNameAsync(_userService.UserId ?? string.Empty);

            var bestellungEntity = await _context.Bestellungen.FindAsync(new object[] { request.Id }, cancellationToken);

            if (bestellungEntity == null)
            {
                throw new KeyNotFoundException($"Bestellung nicht gefunden mit der ID: {request.Id}");
            }

            bestellungEntity.BestelltZu = request.BestelltZu;
            bestellungEntity.Bestellpositionen = request.Bestellpositionen;
            bestellungEntity.LastModified = _dateTime.Now;
            bestellungEntity.LastModifiedBy = user;

            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
