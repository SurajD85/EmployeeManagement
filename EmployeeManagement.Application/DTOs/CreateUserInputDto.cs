using EmployeeManagement.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs
{
    public class CreateUserInputDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(8)]
        public required string Password { get; set; }
        public UserRole Role { get; set; }

        // Add other properties as needed
        public string? EmployeeNumber { get; set; }
        public int? CompanyId { get; set; }
    }
}
