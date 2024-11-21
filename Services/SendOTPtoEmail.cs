using System.Net.Mail;
using System.Net;

namespace Tiki_Web_Project.Services
{
    public class SendOTPtoEmail
    {
        private readonly IConfiguration _configuration;

        public SendOTPtoEmail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var smtpConfig = _configuration.GetSection("Smtp");
            var smtpClient = new SmtpClient
            {
                Host = smtpConfig["Host"],
                Port = int.Parse(smtpConfig["Port"]),
                EnableSsl = bool.Parse(smtpConfig["EnableSSL"]),
                Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"])
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(smtpConfig["Username"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }

    }



}
