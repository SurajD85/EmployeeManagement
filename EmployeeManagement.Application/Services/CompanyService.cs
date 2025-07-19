using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;

namespace EmployeeManagement.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;
        public CompanyService(AppDbContext context)
        {
            _context = context;
        }

        public Task<Company> CreateCompanyAsync(CreateCompanyInputDto input)
        {
            throw new NotImplementedException();
        }

        public Task<List<Company>> GetAllCompaniesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
