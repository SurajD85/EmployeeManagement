using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IInvitationRepository
    {
        Task<Invitation> CreateInvitationAsync(string email, UserRole role, int? companyId, int invitedBy,string token, InvitationStatus status);
        Task<Invitation?> GetInvitationByTokenAsync(string token);
        Task<bool> CancelInvitationAsync(int invitationId);
        Task<bool> CompleteInvitationAsync(int invitationId);
        Task<List<Invitation>> GetPendingInvitationsAsync(int? companyId);
    }
}
