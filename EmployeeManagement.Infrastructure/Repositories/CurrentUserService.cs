using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EmployeeManagement.Infrastructure.Repositories
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public int UserId =>
       int.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? throw new UnauthorizedAccessException());

        public UserRole Role =>
            Enum.Parse<UserRole>(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)
                               ?? throw new UnauthorizedAccessException());

        public int? CompanyId
        {
            get
            {
                var companyClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue("CompanyId");
                return string.IsNullOrEmpty(companyClaim) ? null : int.Parse(companyClaim);
            }
        }

        public string Email =>
            _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)
            ?? throw new UnauthorizedAccessException();
    }
}
