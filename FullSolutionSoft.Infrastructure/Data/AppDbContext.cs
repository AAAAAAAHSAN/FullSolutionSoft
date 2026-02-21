using FullSolutionSoft.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullSolutionSoft.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasIndex(c => c.Email)
            .IsUnique();

        builder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
    }
}
