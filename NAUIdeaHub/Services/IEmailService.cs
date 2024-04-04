using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public interface IEmailService
    {
        public Task ExecuteEmailServiceAsync(Request Idea);
    }
}
