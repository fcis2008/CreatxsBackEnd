using Creatxs.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_configuration["Email:SmtpHost"], int.Parse(_configuration["Email:SmtpPort"]))
            {
                Credentials = new System.Net.NetworkCredential(_configuration["Email:SmtpUser"], _configuration["Email:SmtpPass"]),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}