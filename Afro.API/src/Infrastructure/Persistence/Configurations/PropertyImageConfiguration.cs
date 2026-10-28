using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afro.API.src.Infrastructure.Persistence.Configurations;

public class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
{
    public void Configure(EntityTypeBuilder<PropertyImage> builder)
    {
        builder.ToTable("PropertyImages");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Url)
            .IsRequired()
            .HasMaxLength(2000);
        
        builder.Property(r => r.StorageProvider)
            .HasConversion<int>()
            .IsRequired();
        builder.Property(x => x.StorageKey)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(r => r.SortOrder)
            .IsRequired();
        
        builder.Property(x => x.IsCover)
            .IsRequired();
        
        builder.HasOne(pi => pi.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(a => a.PropertyId);

        builder.HasIndex(x => new
        {
            x.PropertyId,
            x.SortOrder
        });

    }
}
