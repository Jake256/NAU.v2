using Mapster;
using NAUIdeaHub.Models;
using NAUIdeaHub.Repositories;

namespace NAUIdeaHub.Services
{
    public class IdeaHubService : IIdeaHubService
    {
        //------------------------------------------------EXAMPLE CODE----------------------------------------------------------
        //Private Members - Same as in the service but for our Repo
        private readonly IIdeaHubRepository _repository;

        //Constructor
        public IdeaHubService(IIdeaHubRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Request>> GetIdeasAsync()
        {
            var idea = await _repository.GetIdeasAsync();
            return idea.Adapt<IEnumerable<Request>>().ToList(); //This is where mapster is need
        }
        public async Task<IEnumerable<Request>> GetCompletedIdeasAsync()
        {
            var idea = await _repository.GetCompletedIdeasAsync();
            return idea.Adapt<IEnumerable<Request>>().ToList(); //This is where mapster is need
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var user = await _repository.GetUsersAsync();
            return user.Adapt<IEnumerable<User>>().ToList(); //This is where mapster is need
        }
        //----------------------------------------------END EXAMPLE CODE----------------------------------------------------------
    }
}
