using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Application.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IEmailService _emailService;

        public InvitationService(IInvitationRepository invitationRepository, IEmailService emailService)
        {
            _invitationRepository = invitationRepository;
            _emailService=emailService;
        }
        private static string GenerateUniqueToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
        public async Task<bool> CancelInvitationAsync(int invitationId)
        {
            return await _invitationRepository.CancelInvitationAsync(invitationId);
        }

        public async Task<bool> CompleteInvitationAsync(int invitationId)
        {
            return await _invitationRepository.CompleteInvitationAsync(invitationId);
        }

        public async Task<Invitation> CreateInvitationAsync(string email, UserRole role, int? companyId, int invitedBy)
        {

            var token = GenerateUniqueToken();
            var result = await _invitationRepository.CreateInvitationAsync(email, role, companyId, invitedBy, token, InvitationStatus.Pending);

            // Send email
            await _emailService.SendInvitationEmailAsync(email, token);
            return result;
        }

       
        public async Task<Invitation?> GetInvitationByTokenAsync(string token)
        {
            return await _invitationRepository.GetInvitationByTokenAsync(token);
        }

        public async Task<List<Invitation>> GetPendingInvitationsAsync(int? companyId)
        {
            return await _invitationRepository.GetPendingInvitationsAsync(companyId);   
        }
    }
}
