using Microsoft.EntityFrameworkCore;
using Gunpla_600.Domain.Entities;

namespace Gunpla_600.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Users> Users => Set<Users>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique(); // Email unique
        });
    }
}