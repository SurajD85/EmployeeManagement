using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendInvitationEmailAsync(string email, string token, UserRole role, string? companyName);
    }

}
