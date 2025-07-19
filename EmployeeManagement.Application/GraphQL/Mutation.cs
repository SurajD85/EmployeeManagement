using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.GraphQL
{
    public class Mutation
    {
        public async Task<User> CreateUser([Service] IUserService userService, CreateUserInputDto input)
        {
            return await userService.CreateUserAsync(input); // Ensure this matches the method name
        }

        public async Task<Company> CreateCompany([Service] ICompanyService companyService, CreateCompanyInputDto input)
        {
            return await companyService.CreateCompanyAsync(input);
        }
    }

}
