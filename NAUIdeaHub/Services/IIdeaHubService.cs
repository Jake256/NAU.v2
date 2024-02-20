using NAUCountryIdeaHub.Models;

namespace NAUCountryIdeaHub.Services
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
