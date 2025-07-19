using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public Employee EmployeeProfile { get; set; } = null!; // 1-to-1 required
        public List<CompanyUser> CompanyUsers { get; set; } = new();
    }
}
