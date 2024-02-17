using Mapster;
using NAUCountryIdeaHub.Models;
using NAUCountryIdeaHub.Repositories;

namespace NAUCountryIdeaHub.Services
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
        //----------------------------------------------END EXAMPLE CODE----------------------------------------------------------
    }
}
