using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> CreateCompanyAsync(CreateCompanyInputDto input);
        Task<List<Company>> GetAllCompaniesAsync();
        // Other company-related methods...
    }
}
