using EmployeeManagement.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities
{
    public class Invitation
    {
        public int Id { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public UserRole Role { get; set; }
        public int? CompanyId { get; set; } // Null for admin invites

        [MaxLength(64)]
        public required string Token { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);

        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;

        // Navigation properties
        public Company? Company { get; set; }
        public User? InvitedBy { get; set; }
        public int InvitedById { get; set; }
    }
}
