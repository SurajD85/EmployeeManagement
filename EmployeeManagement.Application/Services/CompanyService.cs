using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<Company> CreateCompanyAsync(Company company)
        {
           return _companyRepository.CreateCompanyAsync(company);
        }

        public Task<bool> DeleteCompanyAsync(int id)
        {
            return _companyRepository.DeleteCompanyAsync(id);
        }

        public Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            return _companyRepository.GetAllCompaniesAsync();
        }

        public Task<Company> GetCompanyByIdAsync(int id)
        {
            return _companyRepository.GetCompanyByIdAsync(id);
        }

        public Task<Company> UpdateCompanyAsync(Company company)
        {
            return _companyRepository.UpdateCompanyAsync(company);
        }
    }
}
