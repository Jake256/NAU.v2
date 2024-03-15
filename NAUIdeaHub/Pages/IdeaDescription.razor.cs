using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Models;
using NAUCountryIdeaHub.Services;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaDescription : ComponentBase
    {
        
        [Inject] private IIdeaHubService _service { get; set; }
        // This will be used to return all of the ideas and the actions on them

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();
        //IEnumerable to hold all of the ideas
        public IEnumerable<RequestActions> IdeaActions { get; set; }
        // IEnumerable to hold all of the actions on the ideas

        [Inject] private ILoggedUserService _loggedUser { get; set; }
        // This will be used to grab the current logged in user

        public User loggedInUser;
        public Request currentIdea;
        // Two objects to hold the current logged in user and the current idea that we are getting the description of.

        public bool notLiked;
        // This object will be used to determine if the current user has already liked the idea already.

        protected override async Task OnInitializedAsync()
        {
            try
            {

                Ideas = await _service.GetIdeasAsync();
                IdeaActions = await _service.GetActionsAsync(1);
                // Grabs all of the ideas and the actions on a single idea.
                // Right now I'm just looking at the idea with the description of 1, later on we will pass the id
                // value to this page.                

                currentIdea = Ideas.FirstOrDefault(x => x.RequestID == 1);
                // Grabs the idea that we are currently looking at.
                // Again this is only referring to the idea with the id of 1, and will be improved later.

                if (_loggedUser.getUser() != null)
                {
                    loggedInUser = _loggedUser.getUser();
                    notLiked = IdeaActions.FirstOrDefault(x => x.UserID == loggedInUser.UserID && x.UpVote == true) != null;
                    // Grabs the current user and checks to see if they already liked the idea or not.
                }
                else
                {
                    // Some code goes here
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
