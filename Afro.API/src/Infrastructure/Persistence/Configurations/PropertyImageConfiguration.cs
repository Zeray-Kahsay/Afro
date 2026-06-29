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
        
        builder.Property(r => r.SortOrder)
            .IsRequired();
        
        builder.HasOne(pi => pi.Property)
            .WithMany(p => p.Images)
            .HasForeignKey(pi => pi.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(a => a.PropertyId);

    }
}
