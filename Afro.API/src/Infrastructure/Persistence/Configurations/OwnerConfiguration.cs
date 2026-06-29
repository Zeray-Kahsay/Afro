using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afro.API.src.Infrastructure.Persistence.Configurations;

public sealed class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.PhoneNumber)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(r => r.Email)
            .HasMaxLength(200);

        builder.Property(r => r.Address)
            .HasMaxLength(500);

        builder.Property(r => r.Notes)
            .HasMaxLength(4000);

        builder.HasIndex(r => r.PhoneNumber)
            .IsUnique();
        builder.HasIndex(r => r.FullName);
    }
}
