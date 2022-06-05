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
    public record CreateBestellungCommand : IRequest<Guid>
    {
        public Guid UserId { get; init; }
        public DateTime BestelltZu { get; init; }
        public IList<Bestellposition> Bestellpositionen { get; init; } = new List<Bestellposition>();
    }

    public class CreateBestellungCommandHandler : IRequestHandler<CreateBestellungCommand, Guid>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IApplicationDbContext _context;
        public CreateBestellungCommandHandler(IDateTimeService dateTime, IApplicationDbContext context)
        {
            _dateTime = dateTime;
            _context = context;
        }
        public async Task<Guid> Handle(CreateBestellungCommand request, CancellationToken cancellationToken)
        {
            var newBestellungEntity = new Domain.Entities.Bestellung()
            {
                BestelltZu = request.BestelltZu,
                Bestellpositionen = request.Bestellpositionen,
                UserId = request.UserId,
                CreatedUtc = _dateTime.Now,
                LastModified = _dateTime.Now
            };
            _context.Bestellungen.Add(newBestellungEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return newBestellungEntity.Id;
        }
    }
}
