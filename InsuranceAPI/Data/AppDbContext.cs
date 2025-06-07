using Microsoft.EntityFrameworkCore;
using System;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Policy> Policies { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // One-to-Many: Customers → Policies
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Policy)
            .WithMany(p => p.Customers)
            .HasForeignKey(c => c.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Many-to-Many: Policies ↔ Claims
        modelBuilder.Entity<Customer>()
          .HasOne(c => c.Policy)
          .WithMany() // No direct Customers reference in Policy
          .HasForeignKey(c => c.PolicyId)
          .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}