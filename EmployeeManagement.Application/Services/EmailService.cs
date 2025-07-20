using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Net.Mail;
using Path = System.IO.Path;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit.Text;
using System;
using System.IO;
using System.Threading.Tasks;

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

        public async Task SendInvitationEmailAsync(string email, string token, UserRole role, string? companyName)
        {
            try
            {
                var appUrl = _config["AppSettings:BaseUrl"];
                var invitationUrl = $"{appUrl}/accept-invitation?token={token}";
                var fromName = _config["EmailSettings:FromName"] ?? "Employee Management";
                var fromEmail = _config["EmailSettings:FromEmail"];
                var gmailPassword = _config["EmailSettings:GmailPassword"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromEmail));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = $"Invitation to join {companyName ?? "our system"}";

                // HTML email template
                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto;'>
                            <h2 style='color: #1976d2;'>You've been invited!</h2>
                            <p>You've been invited to join <strong>{companyName ?? "our system"}</strong> as a <strong>{role}</strong>.</p>
                            <p style='margin: 20px 0;'>
                                <a href='{invitationUrl}' 
                                   style='background-color: #1976d2; color: white; 
                                          padding: 10px 20px; text-decoration: none; 
                                          border-radius: 4px;'>
                                    Accept Invitation
                                </a>
                            </p>
                            <p>Or copy this link to your browser: <br/>
                               <code style='word-break: break-all;'>{invitationUrl}</code>
                            </p>
                            <p style='font-size: 12px; color: #777;'>
                                This invitation link will expire in 7 days.
                            </p>
                        </div>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                if (_config.GetValue<bool>("EmailSettings:UseDevelopmentSettings"))
                {
                    // Development mode - save to file
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "sent-emails");
                    Directory.CreateDirectory(savePath);
                    await message.WriteToAsync(Path.Combine(savePath, $"{Guid.NewGuid()}.eml"));
                    _logger.LogInformation("DEV MODE: Email saved to file (not sent)");
                }
                else
                {
                    // Production - Connect to Gmail SMTP
                    await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);

                    // Note: Use AuthenticationMechanisms to authenticate
                    await smtp.AuthenticateAsync(fromEmail, gmailPassword);

                    await smtp.SendAsync(message);
                    await smtp.DisconnectAsync(true);

                    _logger.LogInformation("Invitation email sent to {Email}", email);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send invitation email to {Email}", email);
                throw new ApplicationException("Failed to send invitation email. Please try again later.", ex);
            }
        }
    }
}
