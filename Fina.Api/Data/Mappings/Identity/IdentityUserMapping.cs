using Fina.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings.Identity;

public class IdentityUserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("IdentityUser");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.NormalizedUserName).IsUnique();
        builder.HasIndex(x => x.NormalizedEmail).IsUnique();

        builder.Property(x => x.Email).HasMaxLength(180);
        builder.Property(x => x.NormalizedEmail).HasMaxLength(180);
        builder.Property(x => x.UserName).HasMaxLength(180);
        builder.Property(x => x.NormalizedUserName).HasMaxLength(180);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);
        builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();

        builder.HasMany<IdentityUserClaim<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserLogin<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserToken<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        builder.HasMany<IdentityUserRole<Guid>>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
    }
}
