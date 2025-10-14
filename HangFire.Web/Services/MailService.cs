using SendGrid.Helpers.Mail;
using SendGrid;

namespace HangFire.Web.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Sender(string userMail, string message)
        {
            var apiKey = _configuration.GetSection("APIs")["SendGrid"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ahmetkadirbatlar@gmail.com", "Example User");
            var subject = "www.mysite.com info";
            var to = new EmailAddress(userMail, "Example User");
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = $"<strong>{message}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            var result = await client.SendEmailAsync(msg);
        }
    }
}