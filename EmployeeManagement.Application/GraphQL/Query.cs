using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.GraphQL
{
    public class Query
    {
        public async Task<List<User>> GetUsers([Service] IUserService userService)
        {
            return await userService.GetAllUsersAsync();
        }
        public async Task<List<Company>> GetCompanies([Service] ICompanyService companyService)
        {
            return await companyService.GetAllCompaniesAsync();
        }

    }
}
