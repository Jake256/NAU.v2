using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Models;
using NAUCountryIdeaHub.Services;
using NAUIdeaHub.Services;
using System.Numerics;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaDescription : ComponentBase
    {
        [Inject] private NavigationManager navManager { get; set; }

        [Inject] private IIdeaHubService _service { get; set; }
        // This will be used to return all of the ideas and the actions on them

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();
        //IEnumerable to hold all of the ideas
        public IEnumerable<RequestActions> IdeaActions { get; set; }
        // IEnumerable to hold all of the actions on the ideas

        [Inject] private ILoggedUserService _loggedUser { get; set; }
        // This will be used to grab the current logged in user

        [Parameter] public string id { get; set; }
        // This will be used to grab the id from routing. We will query the database on this value

        public User loggedInUser;
        public Request currentIdea;
        // Two objects to hold the current logged in user and the current idea that we are getting the description of.

        public bool liked;
        public bool favorited;
        // This object will be used to determine if the current user has already liked/favorite the idea already.

        public bool existsInDB;
        // This object is used to see if the user is in the actions db for the current idea.
        // Will use this value to determine if we need to make a new insertion into database or just alter a value.

        protected override async Task OnInitializedAsync()
        {
            try
            {

                Ideas = await _service.GetIdeasAsync();
                IdeaActions = await _service.GetActionsAsync(int.Parse(id));
                // Grabs all of the ideas and the actions on a single idea.


                currentIdea = Ideas.FirstOrDefault(x => x.RequestID == int.Parse(id));
                // Grabs the idea that we are currently looking at.

                if (_loggedUser.getUser() != null)
                {
                    loggedInUser = _loggedUser.getUser();

                    if (IdeaActions.FirstOrDefault(x => x.UserID == loggedInUser.UserID && x.UpVote == true) != null)
                    {
                        liked = true;
                        existsInDB = true;
                    }
                    else if (IdeaActions.FirstOrDefault(x => x.UserID == loggedInUser.UserID && x.UpVote == false) != null)
                    {
                        liked = false;
                        existsInDB = true;
                    }
                    else
                    {
                        liked = false;
                        existsInDB = false;
                    }

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

        /*
         * Method for dealing with the like button code.
         * Will write/update database values based on if the user is in the actions database.
         */
        public void like()
        {
            if (existsInDB) // Checks to see if the user exists in the actions database.
            {
                _service.AlterLike(currentIdea.RequestID, loggedInUser.UserID, 1);
                // Uses the AlterLike method to edit the current database value to create a like.
                liked = true;
                // Sets the liked bool to true to state that the user likes the idea.
            }
            else // User doesn't exist in the actions database
            {
                _service.LikeIdea(currentIdea.RequestID, loggedInUser.UserID);
                // Uses the LikeIdea method which will create a new entry in the database with the liked value set to true.
                liked = true;
                existsInDB = true;
                // Changes the bool values to reflect the changes.
            }

        }

        /*
         * Method for dealing with unlike events.
         * Assumes that the user must exist in the actions database
         */
        public void unLike()
        {
            if (existsInDB) // Checks to see if the user exists.
            {
                _service.AlterLike(currentIdea.RequestID, loggedInUser.UserID, 0);
                // Updates the database to remove the like from the users account
                liked = false;
            }

        }

        /*
         * Method for dealing with the favorite events.
         * Very similar to the like() method.
         */
        public void favorite()
        {
            if (existsInDB) // Checks to see if the user exists.
            {
                _service.AlterFavorite(currentIdea.RequestID, loggedInUser.UserID, 1);
                // Alters the favorite value of the users account.
                favorited = true;
            }
            else // User doesn't exist.
            {
                _service.FavoriteIdea(currentIdea.RequestID, loggedInUser.UserID);
                // Creates an entry in the actions database for this user with the favorite value set to true.
                favorited = true;
                existsInDB = true;
            }
        }

        /*
         * Method for dealing with the unfavorite events
         * Very similar to the unlike() method
         */
        public void unFavorite()
        {
            if (existsInDB) // Checks to see if the user exists in the database.
            {
                _service.AlterFavorite(currentIdea.RequestID, loggedInUser.UserID, 0);
                favorited = false;
            }
        }

        public void close()
        {

        }

        public void changeURL()
        {

        }

        public void goBack()
        {
            navManager.NavigateTo("idealist");
        }
    }
}
