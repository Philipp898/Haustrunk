using Haustrunk.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Haustrunk.Infrastructure.Persistence.Configurations
{
    public class ArtikelConfiguration : IEntityTypeConfiguration<Artikel>
    {
        public void Configure(EntityTypeBuilder<Artikel> builder)
        {
            builder.ToTable("Artikel");
            builder.HasKey(art => art.Id);

            builder.Property(art => art.Marke)
                    .HasMaxLength(50);

            builder.Property(art => art.Sorte)
                    .HasMaxLength(100);
        }
    }
}
