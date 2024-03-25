using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public interface IIdeaHubService
    {
        //-------------------Example--------------------
        public Task<IEnumerable<Request>> GetIdeasAsync();
        public Task<IEnumerable<Request>> GetCompletedIdeasAsync();
        public Task<IEnumerable<User>> GetUsersAsync();
        //-----------------------------------------------
    }
}
