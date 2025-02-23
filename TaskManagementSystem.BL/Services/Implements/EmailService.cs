using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TaskManagementSystem.BL.Helpers;
using TaskManagementSystem.BL.Services.Abstracts;

namespace TaskManagementSystem.BL.Services.Implements
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _client;
        private readonly MailAddress _from;
        private readonly HttpContext Context;

        public EmailService(IOptions<SmtpOption> option, IHttpContextAccessor _httpContextAccessor)
        {
            var opt = option.Value;
            if (string.IsNullOrWhiteSpace(opt.Host))
            {
                throw new InvalidOperationException("SMTP Host is not configured.");
            }
            if (string.IsNullOrWhiteSpace(opt.Sender))
            {
                throw new InvalidOperationException("SMTP Sender is not configured.");
            }

            _from = new MailAddress(opt.Sender);
            _client = new SmtpClient(opt.Host, opt.Port)
            {
                Credentials = new NetworkCredential(opt.Sender, opt.Password),
                EnableSsl = true
            };
        }

        public void SendEMail(string toEmail, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new MailMessage
                {
                    Subject = subject,
                    Body = body,
                    From = _from,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                _client.Send(mailMessage); // E-poçt göndərilir
            }
            catch (Exception ex)
            {
                Console.WriteLine($"E-poçt göndərilərkən xəta baş verdi: {ex.Message}");
                throw;
            }
        }
    }
}
