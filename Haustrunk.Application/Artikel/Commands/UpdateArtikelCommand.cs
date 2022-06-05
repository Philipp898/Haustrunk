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
        private readonly IDateTimeService _dateTime;
        private readonly IApplicationDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _userService;

        public UpdateArtikelCommandHandler(IDateTimeService dateTime, IApplicationDbContext context, IIdentityService identityService, ICurrentUserService userService)
        {
            _dateTime = dateTime;
            _context = context;
            _identityService = identityService;
            _userService = userService;
        }
        public async Task<Unit> Handle(UpdateArtikelCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserNameAsync(_userService.UserId ?? string.Empty);

            var entity = await _context.Artikel.FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new KeyNotFoundException($"Artikel nicht gefunden mit der ID: {request.Id}");
            }

            entity.Marke = request.Marke;
            entity.Sorte = request.Sorte;
            entity.Gebinde = request.Gebinde;
            entity.LastModified = _dateTime.Now;
            entity.LastModifiedBy = user;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
