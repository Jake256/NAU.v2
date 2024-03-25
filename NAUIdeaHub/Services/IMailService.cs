namespace NAUIdeaHub.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(string RecipientAddress, string Subject, string HTMLBody);
    }
}
