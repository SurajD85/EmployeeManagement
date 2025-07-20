using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Auth;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Enum;

namespace EmployeeManagement.Application.GraphQL.Mutations
{
    public class Mutation
    {
        #region Company Mutations
        public async Task<Company> CreateCompany(
           string name,
           string zipCode,
           string address,
           string phoneNumber,
           string emailAddress,
           string? homepageUrl,
           DateTime establishedDate,
           string? remarks,
           [Service] ICompanyService companyService)
        {
            var company = new Company
            {
                Name = name,
                ZipCode = zipCode,
                Address = address,
                PhoneNumber = phoneNumber,
                EmailAddress = emailAddress,
                HomepageUrl = homepageUrl,
                EstablishedDate = establishedDate,
                Remarks = remarks
            };

            return await companyService.CreateCompanyAsync(company);
        }

        public async Task<Company?> UpdateCompany(
            int id,
            string? name,
            string? zipCode,
            string? address,
            string? phoneNumber,
            string? emailAddress,
            string? homepageUrl,
            DateTime? establishedDate,
            string? remarks,
            [Service] ICompanyService companyService)
        {
            var company = await companyService.GetCompanyByIdAsync(id);
            if (company == null) return null;

            if (name != null) company.Name = name;
            if (zipCode != null) company.ZipCode = zipCode;
            if (address != null) company.Address = address;
            if (phoneNumber != null) company.PhoneNumber = phoneNumber;
            if (emailAddress != null) company.EmailAddress = emailAddress;
            if (homepageUrl != null) company.HomepageUrl = homepageUrl;
            if (establishedDate != null) company.EstablishedDate = establishedDate.Value;
            if (remarks != null) company.Remarks = remarks;

            await companyService.UpdateCompanyAsync(company);
            return company;
        }

        public async Task<bool> DeleteCompany(
            int id,
            [Service] ICompanyService companyService)
        {
            return await companyService.DeleteCompanyAsync(id);
        }
        #endregion

        #region USER

        public async Task<User> CreateUser(
            string email,
            string password,
            UserRole role,
            bool isActive,
            [Service] IUserService userService)
        {
            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = role,
                IsActive = isActive,
                CreatedAt = DateTime.UtcNow
            };

            return await userService.CreateUserAsync(user);
        }

        public async Task<User?> UpdateUser(
            int id,
            string? email,
            UserRole? role,
            bool? isActive,
            [Service] IUserService userService)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null) return null;

            if (email != null) user.Email = email;
            if (role.HasValue) user.Role = role.Value;
            if (isActive.HasValue) user.IsActive = isActive.Value;

            await userService.UpdateUserAsync(user);
            return user;
        }

        public async Task<bool> DeleteUser(
            int id,
            [Service] IUserService userService)
        {
            return await userService.DeleteUserAsync(id);
        }
        #endregion

        #region Invitation
        public async Task<Invitation> CreateInvitation(
        string email,
        UserRole role,
        int? companyId,
        [Service] IInvitationService invitationService,
        [Service] ICurrentUserService currentUser)
        {
            // Additional permission checks
            if (role == UserRole.SystemAdmin && companyId != null)
                throw new GraphQLException("System Admin invitations cannot have company association");

            if (currentUser.Role == UserRole.Manager && role == UserRole.GeneralEmployee)
                throw new GraphQLException("Managers cannot invite admins");

            return await invitationService.CreateInvitationAsync(
                email,
                role,
                companyId,
                currentUser.UserId);
        }

        public async Task<User> AcceptInvitation(
            string token,
            string password,
            [Service] IInvitationService invitationService,
            [Service] IUserService userService)
        {
            var invitation = await invitationService.GetInvitationByTokenAsync(token);
            if (invitation == null || invitation.Status != InvitationStatus.Pending)
                throw new GraphQLException("Invalid or expired invitation");

            var user = new User
            {
                Email = invitation.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Role = invitation.Role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var createdUser = await userService.CreateUserAsync(user);

            if (invitation.CompanyId.HasValue)
            {
                // Add to CompanyUsers table
                await userService.AddUserToCompanyAsync(createdUser.Id, invitation.CompanyId.Value);
            }

            await invitationService.CompleteInvitationAsync(invitation.Id);
            return createdUser;
        }

        public async Task<bool> CancelInvitation(
            int invitationId,
            [Service] IInvitationService invitationService,
            [Service] ICurrentUserService currentUser)
        {
            // Add permission validation
            return await invitationService.CancelInvitationAsync(invitationId);
        }
        #endregion


        #region Login
        public async Task<LoginResponse> Login(
        LoginRequest request,
        [Service] IAuthService authService)
        {
            return await authService.LoginAsync(request);
        }
        #endregion
    }

}
