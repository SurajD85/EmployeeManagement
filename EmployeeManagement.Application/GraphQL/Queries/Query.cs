using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.GraphQL.Queries
{
    public class Query
    {
        #region Companies
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

        public async Task<List<User>> GetActiveUsers(
            [Service] IUserService userService)
        {
            var allUsers = await userService.GetAllUsersAsync();
            return allUsers.Where(u => u.IsActive).ToList();
        }
        #endregion
    }

}
