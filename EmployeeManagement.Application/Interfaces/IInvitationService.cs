using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IInvitationService
    {
        Task<Invitation> CreateInvitationAsync(string email, UserRole role, int? companyId, int invitedBy);
        Task<Invitation?> GetInvitationByTokenAsync(string token);
        Task<bool> CancelInvitationAsync(int invitationId);
        Task<bool> CompleteInvitationAsync(int invitationId);
        Task<List<Invitation>> GetPendingInvitationsAsync(int? companyId);
    }

}
