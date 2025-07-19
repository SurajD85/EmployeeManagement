namespace EmployeeManagement.Domain.Entities
{
    public class CompanyUser
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
        public DateTime JoinedAt { get; set; } = DateTime.Now;
    }

}
