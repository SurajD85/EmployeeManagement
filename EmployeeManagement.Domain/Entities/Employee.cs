using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Remarks { get; set; }
        public string? ProfileImage { get; set; }


        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
