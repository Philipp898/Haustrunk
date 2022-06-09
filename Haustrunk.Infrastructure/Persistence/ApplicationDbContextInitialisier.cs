using Haustrunk.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Haustrunk.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbContextInitialiser(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitialiseAsync()
        {

            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }

        }

        public async Task SeedAsync()
        {
            await TrySeedAsync();
        }

        private async Task TrySeedAsync()
        {
            // Standardbenutzer
            var defaultNutzer = new ApplicationUser { UserName = "philipp@localhost", Email = "philipp@localhost" };

            if (_userManager.Users.All(u => u.UserName != defaultNutzer.UserName))
            {
                await _userManager.CreateAsync(defaultNutzer, "Administrator1!");
            }

            //Standarddaten
            if (!_context.Artikel.Any())
            {
                _context.Artikel.Add(new Domain.Entities.Artikel() 
                { 
                    Marke = "Ratsherrn", 
                    Sorte = "Export", 
                    Gebinde = Domain.Enums.Gebinde.VierundzwanzigMalNullDreiDrei,
                    CreatedBy = defaultNutzer.UserName,
                });
                _context.Artikel.Add(new Domain.Entities.Artikel()
                {
                    Marke = "Loona",
                    Sorte = "Naturelle",
                    Gebinde = Domain.Enums.Gebinde.ZwölfMalNullSiebenFünf,
                    CreatedBy = defaultNutzer.UserName,

                });
                _context.Artikel.Add(new Domain.Entities.Artikel()
                {
                    Marke = "Lübzer",
                    Sorte = "Export",
                    Gebinde = Domain.Enums.Gebinde.ZwanzigMalNullFünf,
                    CreatedBy = defaultNutzer.UserName,
                });
                await _context.SaveChangesAsync();

            }

            if (!_context.Bestellungen.Any())
            {
                _context.Bestellungen.Add(new Domain.Entities.Bestellung()
                {
                    BestelltZu = new DateTime(2022, 06, 09),
                    CreatedBy = defaultNutzer.UserName,
                });
                await _context.SaveChangesAsync();

            }
            if (!_context.Bestellpositionen.Any())
            {
                _context.Bestellpositionen.Add(new Domain.Entities.Bestellposition()
                {
                    Bestellmenge = 2,
                    ArtikelId = _context.Artikel.Where(art => art.Marke == "Lübzer").Select(art => art.Id).Single(),
                    Bestellung = _context.Bestellungen.Where(best => best.BestelltZu == new DateTime(2022, 06, 09)).Single()
                });
                _context.Bestellpositionen.Add(new Domain.Entities.Bestellposition()
                {
                    Bestellmenge = 1,
                    ArtikelId = _context.Artikel.Where(art => art.Marke == "Ratsherrn").Select(art => art.Id).Single(),
                    Bestellung = _context.Bestellungen.Where(best => best.BestelltZu == new DateTime(2022, 06, 09)).Single()
                });
                _context.Bestellpositionen.Add(new Domain.Entities.Bestellposition()
                {
                    Bestellmenge = 3,
                    ArtikelId = _context.Artikel.Where(art => art.Marke == "Loona").Select(art => art.Id).Single(),
                    Bestellung = _context.Bestellungen.Where(best => best.BestelltZu == new DateTime(2022, 06, 09)).Single()
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
