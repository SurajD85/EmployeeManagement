using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;
using HotChocolate.Authorization;

namespace EmployeeManagement.Application.GraphQL.Queries
{
    [Authorize]
    public class Query
    {
        #region Companies
        //[Authorize]
        public async Task<Company?> GetCompanyById(
            int id,
            [Service] ICompanyService companyService)
        {
            return await companyService.GetCompanyByIdAsync(id);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(
            [Service] ICompanyService companyService)
        {
            return await companyService.GetAllCompaniesAsync();
        }


        #endregion

        #region User
        public async Task<List<User>> GetAllUsers(
            [Service] IUserService userService)
        {
            return await userService.GetAllUsersAsync();
        }

        public async Task<User?> GetUserById(
            int id,
            [Service] IUserService userService)
        {
            return await userService.GetUserByIdAsync(id);
        }

        public async Task<User?> GetUserByEmail(
           string email,
           [Service] IUserService userService)
        {
            return await userService.GetUserByEmailAsync(email);
        }

        public async Task<List<User>> GetActiveUsers(
            [Service] IUserService userService)
        {
            var allUsers = await userService.GetAllUsersAsync();
            return allUsers.Where(u => u.IsActive).ToList();
        }
        #endregion

        #region Invitations
        public async Task<List<Invitation>> GetPendingInvitations(
        int? companyId,
        [Service] IInvitationService invitationService,
        [Service] ICurrentUserService currentUser)
        {
            // Filter by company if manager
            if (currentUser.Role == UserRole.Manager)
            {
                companyId = currentUser.CompanyId; // Assuming manager has single company
            }

            return await invitationService.GetPendingInvitationsAsync(companyId);
        }

        public async Task<Invitation?> ValidateInvitationToken(
            string token,
            [Service] IInvitationService invitationService)
        {
            return await invitationService.GetInvitationByTokenAsync(token);
        }
        #endregion

    }

}
