using EmployeeManagement.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task SendInvitationEmailAsync(string email, string token)
        {
            try
            {
                var appUrl = _config["AppSettings:BaseUrl"];
                var invitationUrl = $"{appUrl}/accept-invitation?token={token}";

                // In production, use a proper email template
                var subject = "Account Invitation";
                var body = $"You've been invited! Click here to register: {invitationUrl}";

                // Implement actual email sending logic here
                _logger.LogInformation($"Sending invitation email to {email}. URL: {invitationUrl}");

                // Mock implementation
                await Task.Delay(100);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send invitation email");
                throw;
            }
        }
    }
}
