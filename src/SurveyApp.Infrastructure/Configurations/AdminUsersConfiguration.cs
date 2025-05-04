using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyApp.Domain.Entities;

namespace SurveyApp.Infrastructure.Configurations;

public class AdminUsersConfiguration : IEntityTypeConfiguration<AdminUser>
{
    public void Configure(EntityTypeBuilder<AdminUser> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(t => t.Login).IsRequired();
        builder.Property(t => t.PasswordHash).IsRequired();
        builder.Property(t => t.Email).IsRequired();
        
        builder
            .HasMany(a => a.Forms)
            .WithOne(f => f.AdminUser)
            .HasForeignKey(f => f.AdminUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}