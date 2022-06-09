using Duende.IdentityServer.EntityFramework.Options;
using Haustrunk.Application.Common.Interfaces;
using Haustrunk.Domain.Entities;
using Haustrunk.Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Haustrunk.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }
        public DbSet<Artikel> Artikel {get; set;}

        public DbSet<Bestellung> Bestellungen { get; set; }

        public DbSet<Bestellposition> Bestellpositionen { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
