using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.IdentityModel.Tokens;
using NAUIdeaHub.Models;
using NAUIdeaHub.Services;
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

        public IEnumerable<RequestActions> allActions { get; set; } = new List<RequestActions>();
        
        public IEnumerable<User> Users { get; set; } = new List<User>();

        public List<Request> upVotedIdeas { get; set; } = new List<Request>();

        public List<Request> favoritedIdeas { get; set; } = new List<Request>();

        public List<Request> sortedIdeas { get; set; } = new List<Request>();

        [Inject] private ILoggedUserService protectedSessionStore { get; set; }

        public User? authenticatedUser;




        protected override async Task OnInitializedAsync()
        {
            authenticatedUser = await protectedSessionStore.GetUserAsync();

            try
            {
                Users = await _service.GetUsersAsync();
                Ideas = await _service.GetIdeasAsync();
                allActions = await _service.GetAllActionsAsync();

                var ideasWithUpVotesCount = Ideas.Select(idea => new {
                    Idea = idea,
                    upVotesCount = allActions.Count(action => action.RequestID == idea.RequestID && action.UpVote)
                });

                // Sort Ideas based on FavoritesCount
                sortedIdeas = ideasWithUpVotesCount.OrderByDescending(item => item.upVotesCount).Select(item => item.Idea).ToList();


                //CompletedIdeas = await _service.GetCompletedIdeasAsync(); 
                foreach (var x in sortedIdeas)
                {
                    if (x.Closed == true)
                    {
                        CompletedIdeas.Add(x);
                    }
                }

                if(authenticatedUser != null)
                {

                    foreach(var x in sortedIdeas)
                    {
                        foreach(var y in allActions)
                        {
                            if(authenticatedUser.UserID == y.UserID && y.RequestID == x.RequestID && y.UpVote == true)
                            {
                                upVotedIdeas.Add(x);
                            }
                        }
                    }

                    foreach (var x in sortedIdeas)
                    {
                        foreach (var y in allActions)
                        {
                            if (authenticatedUser.UserID == y.UserID && y.RequestID == x.RequestID && y.Favorite == true)
                            {
                                favoritedIdeas.Add(x);
                            }
                        }
                    }


                    foreach (var x in sortedIdeas)
                    {
                        if(x.Requestor == authenticatedUser.UserID)
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

        public void SearchIdeas()
        {
            // Reset sorted ideas to include all ideas
            var ideasWithUpVotesCount = Ideas.Select(idea => new {
                Idea = idea,
                upVotesCount = allActions.Count(action => action.RequestID == idea.RequestID && action.UpVote)
            });

            // Sort Ideas based on FavoritesCount
            sortedIdeas = ideasWithUpVotesCount.OrderByDescending(item => item.upVotesCount).Select(item => item.Idea).ToList();

            // Loop through each Request
            foreach (Request x in sortedIdeas.ToList())
            {
                if (!searchValue.IsNullOrEmpty() && 
                    !x.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase) && 
                    !x.Description.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                {
                    sortedIdeas.Remove(x); // Remove x, name or description does not contain search string
                }
            }
        }

        public void FilterByDate()
        {
            // Reset sorted ideas to include all ideas
            var ideasWithUpVotesCount = Ideas.Select(idea => new {
                Idea = idea,
                upVotesCount = allActions.Count(action => action.RequestID == idea.RequestID && action.UpVote)
            });

            // Sort Ideas based on FavoritesCount
            sortedIdeas = ideasWithUpVotesCount.OrderByDescending(item => item.upVotesCount).Select(item => item.Idea).ToList();

            if(startDate != null && endDate != null) // Check for null values
            {
                foreach(Request r in sortedIdeas.ToList()) // Loop through each Request
                {
                    if(r.DateTimeSubmitted <  startDate || r.DateTimeSubmitted > endDate)
                    {
                        sortedIdeas.Remove(r); // Remove idea, not within date range
                    }
                }
            }
        }
    }
}
