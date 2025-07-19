using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserInputDto input); // Ensure this method is defined

        Task<List<User>> GetAllUsersAsync();
        // Other user-related methods...
    }
}
