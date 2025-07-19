using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;
using EmployeeManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly AppDbContext _context;

        public InvitationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelInvitationAsync(int invitationId)
        {
            var invitation = await _context.Invitations.FindAsync(invitationId);
            if (invitation == null || invitation.Status != InvitationStatus.Pending)
                return false;

            invitation.Status = InvitationStatus.Cancelled;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CompleteInvitationAsync(int invitationId)
        {
            var invitation = await _context.Invitations.FindAsync(invitationId);
            if (invitation == null || invitation.Status != InvitationStatus.Pending)
                return false;

            invitation.Status = InvitationStatus.Completed;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<Invitation> CreateInvitationAsync(string email, UserRole role, int? companyId, int invitedBy, string token, InvitationStatus status)
        {
            var invitation = new Invitation
            {
                Email = email,
                Role = role,
                CompanyId = companyId,
                Token = token,
                InvitedById = invitedBy,
                Status = InvitationStatus.Pending
            };
             _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();
            return invitation;
        }

        public async Task<Invitation?> GetInvitationByTokenAsync(string token)
        {
            return await _context.Invitations
             .Include(i => i.Company)
             .FirstOrDefaultAsync(i => i.Token == token && i.Status == InvitationStatus.Pending);

        }

        public async Task<List<Invitation>> GetPendingInvitationsAsync(int? companyId)
        {
            var query = _context.Invitations
            .Where(i => i.Status == InvitationStatus.Pending);

            if (companyId.HasValue)
            {
                query = query.Where(i => i.CompanyId == companyId);
            }

            return await query.ToListAsync();
        }
    }
}
