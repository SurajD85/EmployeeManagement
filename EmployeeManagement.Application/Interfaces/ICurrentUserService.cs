using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Application.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        UserRole Role { get; }
        int? CompanyId { get; } // Null for admin, populated for managers/employees
        string Email { get; }
    }

}
