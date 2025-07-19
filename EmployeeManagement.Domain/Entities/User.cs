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

        // Navigation property for many-to-many relationship
        public List<Company> Companies { get; set; } = new();
    }
}
