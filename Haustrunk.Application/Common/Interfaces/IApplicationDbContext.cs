using Haustrunk.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Haustrunk.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.Artikel> Artikel { get; }
        DbSet<Bestellung> Bestellungen { get; }
        DbSet<Bestellposition> Bestellpositionen { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
