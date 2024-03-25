using NAUIdeaHub.Entities;

namespace NAUIdeaHub.Repositories
{
    public interface IIdeaHubRepository
    {
        //-------------Example----------------------------------
        public Task<IEnumerable<RequestEntity>> GetIdeasAsync();
        public Task<IEnumerable<RequestEntity>> GetCompletedIdeasAsync();
        public Task<IEnumerable<UserEntity>> GetUsersAsync();
        //------------------------------------------------------
    }
}
