using Afro.API.src.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afro.API.src.Infrastructure.Persistence.Configurations;

public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshAccessToken> 
{
    public void Configure(EntityTypeBuilder<RefreshAccessToken> builder)
    {
        builder.ToTable("RefreshAccessTokens");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token)
            .IsRequired()
            .HasMaxLength(500);
        
        builder.HasIndex(r => r.Token)
            .IsUnique();
        
        builder.HasOne(r => r.User)
            .WithMany(r => r.RefreshAccessTokens)
            .HasForeignKey(r => r.UserId);
    }
}
