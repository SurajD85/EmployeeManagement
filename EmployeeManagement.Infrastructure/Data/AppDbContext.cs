using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Role).IsRequired();
                entity.Property(u => u.IsActive).IsRequired();
                entity.Property(u => u.CreatedAt).IsRequired();
                // 1-to-1 with Employee
                entity.HasOne(u => u.EmployeeProfile)
                      .WithOne(e => e.User)
                      .HasForeignKey<Employee>(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });


            // Employee
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmployeeNumber).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                // Other properties optional
            });
            // Company
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
                entity.Property(c => c.EmailAddress).IsRequired();
                entity.Property(c => c.EstablishedDate).IsRequired();
            });

            // CompanyUser Configuration (Many-to-Many)
            modelBuilder.Entity<CompanyUser>(entity =>
            {
                entity.HasKey(cu => new { cu.UserId, cu.CompanyId });

                entity.Property(cu => cu.JoinedAt)
                    .IsRequired();

                // Relationship with User
                entity.HasOne(cu => cu.User)
                    .WithMany(u => u.CompanyUsers)
                    .HasForeignKey(cu => cu.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Relationship with Company
                entity.HasOne(cu => cu.Company)
                    .WithMany(c => c.CompanyUsers)
                    .HasForeignKey(cu => cu.CompanyId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Additional index
                entity.HasIndex(cu => cu.CompanyId);
            });

            // Additional configurations (e.g., indexes, if needed)
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Company>().HasIndex(c => c.Name).IsUnique();

            // Configure Invitation entity
            modelBuilder.Entity<Invitation>(entity =>
            {
                entity.HasIndex(i => i.Token).IsUnique();
                entity.HasIndex(i => i.Email);

                entity.Property(i => i.Status)
                    .HasConversion<string>();

                entity.HasOne(i => i.Company)
                    .WithMany()
                    .HasForeignKey(i => i.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(i => i.InvitedBy)
                    .WithMany()
                    .HasForeignKey(i => i.InvitedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }


        public async Task SeedAsync()
        {
            if (!Users.Any())
            {
                var superUser = new User
                {
                    Email = "superadmin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Super@123"), // Use a secure password
                    Role = UserRole.SystemAdmin,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };
                Users.Add(superUser);
                await SaveChangesAsync();
            }
        }

    }
}