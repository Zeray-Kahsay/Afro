using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afro.API.src.Infrastructure.Persistence.Configurations;

public sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(p => p.Price)
            .HasPrecision(18, 2);
        
        builder.Property(p => p.Country)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.City)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.AddressLine)
            .HasMaxLength(500);
        
        builder.Property(p => p.Area)
            .HasPrecision(18, 2);
        
        builder.Property(p => p.Purpose)
            .HasConversion<int>();
        
        builder.Property(p => p.Type)
            .HasConversion<int>();

        builder.Property(p => p.Status)
            .HasConversion<int>();
        
        builder.Property(p => p.CreatedAtUtc)
            .IsRequired();
        
        builder.Property(p => p.UpdatedAtUtc)
            .IsRequired();
        
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.Type);
        builder.HasIndex(p => p.Purpose);
        builder.HasIndex(p => p.City);
        builder.HasIndex(p => p.Price);

        builder.HasOne(p => p.Owner)
            .WithMany(ow => ow.Properties)
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
