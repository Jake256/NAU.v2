using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Models;
using NAUCountryIdeaHub.Services;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaList : ComponentBase
    {
        [Inject] private IIdeaHubService _service { get; set; }

        [Inject] private NavigationManager navManager { get; set; }

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();

        public List<Request> CompletedIdeas { get; set; } = new List<Request>();

        public List<Request> YourIdeas { get; set; } = new List<Request>();
        
        public IEnumerable<User> Users { get; set; } = new List<User>();

        

        [Inject] private ILoggedUserService _loggedUser { get; set; }

        public User loggedInUser;


        protected override async Task OnInitializedAsync()
        {
            try
            {
                Users = await _service.GetUsersAsync();
                Ideas = await _service.GetIdeasAsync();

                //CompletedIdeas = await _service.GetCompletedIdeasAsync(); 
                foreach (var x in Ideas)
                {
                    if (x.Closed == true)
                    {
                        CompletedIdeas.Add(x);
                    }
                }


               
                if (_loggedUser.getUser() != null)
                {
                    loggedInUser = _loggedUser.getUser();
                }
                else 
                { 
                    //some code goes here
                }

                if(loggedInUser != null)
                {
                    foreach(var x in Ideas)
                    {
                        if(x.Requestor == loggedInUser.UserID)
                        {
                            YourIdeas.Add(x);
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*
         * This method will route to the idea description page when the user clicks on the learn more button in the card.
         * This will then route to the page with the ideas id as the input.
         * 
         * Ex:
         * We want to go to an idea with the id of 4. Once the user clicks the learn more button the route will look like this:
         *      ideadescription/4
         */
        public void learnMore(int id)
        {
            navManager.NavigateTo("ideadescription/" + id);
        }

    }
}
