using InsuranceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Data
{
    public class InsuranceDbContext : DbContext
    {
        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Policy> Policies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Policy configurations
            modelBuilder.Entity<Policy>()
                .HasKey(p => p.PolicyId);

            modelBuilder.Entity<Policy>()
                .Property(p => p.Type)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Policy>()
                .Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Policy>()
                .Property(p => p.Coverage)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Policy>()
                .Property(p => p.Premium)
                .HasPrecision(18, 2);

            // Customer configurations
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Claim configurations
            modelBuilder.Entity<Claim>()
                .HasKey(c => c.ClaimId);

            modelBuilder.Entity<Claim>()
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Claim>()
                .Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Claim>()
                .Property(c => c.Amount)
                .HasPrecision(18, 2);

            // Payment configurations
            modelBuilder.Entity<Payment>()
                .HasKey(p => p.PaymentId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.PaymentMethod)
                .IsRequired()
                .HasMaxLength(20);

            modelBuilder.Entity<Payment>()
                .Property(p => p.TransactionId)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(20);

            // Relationships
            modelBuilder.Entity<Policy>()
                .HasMany(p => p.Customers)
                .WithMany(c => c.Policies);

            modelBuilder.Entity<Policy>()
                .HasMany(p => p.Claims)
                .WithMany(c => c.Policies);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Payments)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Policy>()
                .HasMany(p => p.Payments)
                .WithOne(p => p.Policy)
                .HasForeignKey(p => p.PolicyId);
        }
    }
} 