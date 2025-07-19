using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Infrastructure.Data;

namespace EmployeeManagement.Application.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

       
        public Task<User> CreateUserAsync(CreateUserInputDto input)
        {
            throw new NotImplementedException();
        }

     

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
