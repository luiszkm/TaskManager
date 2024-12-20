using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Infra.EFCore.Persistence.Configurations;

namespace TaskManager.Infra.EFCore;

public class TaskManegDbContext : DbContext
{
    public TaskManegDbContext(
        DbContextOptions<TaskManegDbContext> options) : base(options) { }
    public DbSet<TaskUser> Tasks => Set<TaskUser>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DBUserConfigurations());
        modelBuilder.ApplyConfiguration(new DBTaskConfigurations());

    }
}
