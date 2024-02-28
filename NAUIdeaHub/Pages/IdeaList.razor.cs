using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Models;
using NAUCountryIdeaHub.Services;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaList : ComponentBase
    {
        [Inject] private IIdeaHubService _service { get; set; }

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();
        public IEnumerable<Request> CompletedIdeas { get; set; } = new List<Request>();
        [Inject] private ILoggedUserService _loggedUser { get; set; }

        public User loggedInUser;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Ideas = await _service.GetIdeasAsync();
                CompletedIdeas = await _service.GetCompletedIdeasAsync(); 
                if( _loggedUser.getUser() != null)
                {
                    loggedInUser = _loggedUser.getUser();
                }
                else
                {
                    // Some code goes here
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
