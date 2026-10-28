using Afro.API.src.Modules.Listings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afro.API.src.Infrastructure.Persistence.Configurations;

public sealed class PropertyImageUploadIntentConfiguration : IEntityTypeConfiguration<PropertyImageUploadIntent>
{
    public void Configure(EntityTypeBuilder<PropertyImageUploadIntent> builder)
    {
        builder.ToTable("PropertyImageUploadIntent");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.StorageProvider)
            .HasConversion<int>()
            .IsRequired();
        
        builder.Property(x => x.StorageKey)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.CreatedAtUtc)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.Property(x => x.FinalizedAtUtc)
            .IsRequired(false);
        
        builder.HasOne(x => x.Property)
            .WithMany()
            .HasForeignKey(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(x => x.PropertyId);

        builder.HasIndex(x => new
        {
            x.StorageProvider,
            x.StorageKey
        }).IsUnique();
    }
}
