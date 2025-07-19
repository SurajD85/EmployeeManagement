using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            // Simple validation example
            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentException("Email cannot be empty");

            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
            if (existingUser == null) return false;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return false;

            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> AddUserToCompanyAsync(int userId, int companyId)
        {
            return await _userRepository.AddToCompanyAsync(userId, companyId);
        }
    }
}
