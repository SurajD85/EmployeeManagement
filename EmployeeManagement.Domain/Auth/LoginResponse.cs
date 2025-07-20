using EmployeeManagement.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Auth
{
    public class LoginResponse
    {
        public required string Token { get; set; }
        public DateTime Expiry { get; set; }
        public UserRole Role { get; set; }
    }

}
