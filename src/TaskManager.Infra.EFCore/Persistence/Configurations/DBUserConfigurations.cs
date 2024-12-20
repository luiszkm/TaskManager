using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Entities;

namespace TaskManager.Infra.EFCore.Persistence.Configurations;

public class DBUserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName).HasMaxLength(50).IsRequired();
        builder.Property(u => u.PasswordHash).HasMaxLength(500).IsRequired();
        builder.HasMany(u => u.Tasks).WithOne().HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
