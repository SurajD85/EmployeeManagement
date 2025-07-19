using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? HomepageUrl { get; set; }
        public DateTime EstablishedDate { get; set; } = DateTime.UtcNow;
        public string? Remarks { get; set; }
        // Navigation properties
        public List<CompanyUser> CompanyUsers { get; set; } = new(); // Many-to-many junction
        public List<Employee> Employees { get; set; } = new(); // Employees directly belong
    }
}
