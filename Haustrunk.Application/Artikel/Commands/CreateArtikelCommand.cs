using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haustrunk.Application.Artikel.Commands
{
    public record CreateArtikelCommand : IRequest<Guid>
    {
        public string Marke { get; init; } = string.Empty;
        public string Sorte { get; init; } = string.Empty;
        public Gebinde Gebinde { get; init; }
    }
    public class CreateArtikelCommandHandler : IRequestHandler<CreateArtikelCommand, Guid>
    {
        private readonly IDateTimeService _dateTime;
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _userService;

        public CreateArtikelCommandHandler(IDateTimeService dateTime, IApplicationDbContext context, IIdentityService identityService, ICurrentUserService userService)
        {
            _dateTime = dateTime;
            _context = context;
            _identityService = identityService;
            _userService = userService;
        }

        public async Task<Guid> Handle(CreateArtikelCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserNameAsync(_userService.UserId ?? string.Empty);
            var newEntity = new Domain.Entities.Artikel()
            {
                Marke = request.Marke,
                Sorte = request.Sorte,
                Gebinde = request.Gebinde,
                CreatedUtc = _dateTime.Now,
                CreatedBy = user,
                LastModified = _dateTime.Now,
                LastModifiedBy = user
            };
            _context.Artikel.Add(newEntity);
            await _context.SaveChangesAsync(cancellationToken);
            return newEntity.Id;
        }
    }
}
