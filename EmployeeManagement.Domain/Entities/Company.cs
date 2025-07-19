using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? HomepageUrl { get; set; }
        public DateTime? EstablishedDate { get; set; }
        public string? Remarks { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}
