using Afro.API.src.Modules.Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afro.API.src.Infrastructure.Persistence.Configurations;

public sealed class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(r => r.IsActive)
            .IsRequired();
        
        builder.Property(r => r.CreatedAt)
            .IsRequired();
        
        
    }
}
