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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships and constraints
            modelBuilder.Entity<User>()
                .HasMany(u => u.Companies)
                .WithMany();
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Company)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.CompanyId);
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