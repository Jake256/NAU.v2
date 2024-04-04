using SendGrid;
using SendGrid.Helpers.Mail;
using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public class EmailService : IEmailService
    {
        public string ApiKey { get; set; } 

        public EmailService(IConfiguration config)
        {
            ApiKey = config.GetConnectionString("SendGridApiKey");
        }

        public async Task ExecuteEmailServiceAsync(Request Idea)
        {
            //var apiKey = Environment.GetEnvironmentVariable("NAU Project API Key");

            var client = new SendGridClient(ApiKey);
            var from = new EmailAddress("jakeriley2020@gmail.com", "NAU");
            var subject = $"Idea {Idea.RequestID} Completed!";
            var to = new EmailAddress("jake256.gaming@gmail.com", "Employee");
            var plainTextContent = "Hello {User}, your NAU Idea has been closed in the Idea Hub! Go check it out!";
            var htmlContent = "<strong>Hello {User}, your NAU Idea has been closed in the Idea Hub! Go check it out!</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
        }
    }
}
