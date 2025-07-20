using EmployeeManagement.Domain.Auth;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }

}
