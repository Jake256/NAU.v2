using Microsoft.AspNetCore.Components;
using NAUIdeaHub.Models;
using NAUIdeaHub.Services;
using System.Numerics;
using System.Security.Principal;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaDescription : ComponentBase
    {
        [Inject] private NavigationManager navManager { get; set; }
        // Allows the transitioning between pages and the ability to reload a page

        [Inject] private IIdeaHubService _service { get; set; }
        // This will be used to return all of the ideas and the actions on them

        [Inject] private IEmailService _emailService { get; set; }

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();
        //IEnumerable to hold all of the ideas

        public IEnumerable<RequestActions> IdeaActions { get; set; }
        // IEnumerable to hold all of the actions on the ideas

        public IEnumerable<RequestNote> IdeaNotes { get; set; }

        public IEnumerable<User> Users { get; set; }

        [Inject] private ILoggedUserService protectedSessionStore { get; set; }
        // This will be used to grab the current logged in user

        [Parameter] public string id { get; set; }
        // This will be used to grab the id from routing. We will query the database on this value

        public User? authenticatedUser;
        public Request currentIdea;
        // Two objects to hold the current logged in user and the current idea that we are getting the description of.

        public bool liked;
        public bool favorited;
        // This object will be used to determine if the current user has already liked/favorite the idea already.

        public bool existsInDB;
        // This object is used to see if the user is in the actions db for the current idea.
        // Will use this value to determine if we need to make a new insertion into database or just alter a value.


        /*
         * This method will run when the page is loaded.
         */
        protected override async Task OnInitializedAsync()
        {
            authenticatedUser = await protectedSessionStore.GetUserAsync();

            try
            {
                Users = await _service.GetUsersAsync();
                Ideas = await _service.GetIdeasAsync();
                IdeaActions = await _service.GetActionsAsync(int.Parse(id));
                IdeaNotes = await _service.GetNotesAsync(int.Parse(id));
                // Grabs all of the ideas and the actions on a single idea.


                currentIdea = Ideas.FirstOrDefault(x => x.RequestID == int.Parse(id));
                // Grabs the idea that we are currently looking at.
                

                // This if statement checks to see if the current user has liked or favorited the current idea
                if (authenticatedUser != null)
                {

                    if (IdeaActions.FirstOrDefault(x => x.UserID == authenticatedUser.UserID && x.UpVote == true) != null)
                    {
                        liked = true;
                        existsInDB = true;
                    }
                    else if (IdeaActions.FirstOrDefault(x => x.UserID == authenticatedUser.UserID && x.UpVote == false) != null)
                    {
                        liked = false;
                        existsInDB = true;
                    }
                    else
                    {
                        liked = false;
                        existsInDB = false;
                    }

                    if (IdeaActions.FirstOrDefault(x => x.UserID == authenticatedUser.UserID && x.Favorite == true) != null)
                    {
                        favorited = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // --------------------------- All Methods for IdeaDescription Logic ---------------------------

        public void goBack()
        {
            navManager.NavigateTo("idealist");
        }

        // -------------------- Methods for Liking/Unliking and Favoriting an Idea ---------------------

        /*
         * Method for dealing with the like button code.
         * Will write/update database values based on if the user is in the actions database.
         */
        public void like()
        {
            if (existsInDB) // Checks to see if the user exists in the actions database.
            {
                _service.AlterLike(currentIdea.RequestID, authenticatedUser.UserID, 1);
                // Uses the AlterLike method to edit the current database value to create a like.
                liked = true;
                // Sets the liked bool to true to state that the user likes the idea.
            }
            else // User doesn't exist in the actions database
            {
                _service.LikeIdea(currentIdea.RequestID, authenticatedUser.UserID);
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
                _service.AlterLike(currentIdea.RequestID, authenticatedUser.UserID, 0);
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
                _service.AlterFavorite(currentIdea.RequestID, authenticatedUser.UserID, 1);
                // Alters the favorite value of the users account.
                favorited = true;
            }
            else // User doesn't exist.
            {
                _service.FavoriteIdea(currentIdea.RequestID, authenticatedUser.UserID);
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
                _service.AlterFavorite(currentIdea.RequestID, authenticatedUser.UserID, 0);
                favorited = false;
            }
        }

        // ---------------- End of Methods for Liking/Unliking and Favoriting an Idea ----------------


        // ------------------------------ Methods Relating to Comments -------------------------------

        /*
         * Pops up the add comment overlay.
         */
        public void addComment()
        {
            updateVisible = true;
            addCommentVisible = true;
        }

        /*
         * Logic to add the comment to the database and refresh the idea description page.
         */
        public void submitComment()
        {
            _service.AddComment(currentIdea.RequestID, commentField.Value, authenticatedUser.UserID);
            addCommentVisible = false;
            navManager.NavigateTo("ideadescription/" + id, true);
        }

        /*
         * Pops up the overlay allowing the user to edit a comment
         */
        public void editComment(int oldCommentID, string oldComment)
        {
            this.oldComment = oldComment;
            this.oldCommentID = oldCommentID;
            // Assigns the values of the original comment to these two objects allowing the user to edit them.

            updateVisible = true;
            editCommentVisible = true;

        }

        /*
         * Pops up the edit comment confirmation overlay before commiting any changes
         */
        public void editCommentConfirmation()
        {
            editCommentVisible = false;
            updateVisible = false;
            editCommentConfirmationVisible = true;
        }

        /*
         * Hides the edit comment confirmation overlay and brings the user back to the edit comment overlay
         */
        public void cancelNewComment()
        {
            editCommentConfirmationVisible = false;
            updateVisible = true;
            editCommentVisible = true;
        }

        /*
         * Commits the changes that the user created to the commnet
         * Makes a database UPDATE call that will change the comment, and uses NavManager to reload the page.
         */
        public void updateComment()
        {
            _service.editComment(oldCommentID, newCommentField.Value);

            editCommentConfirmationVisible = false;
            oldCommentID = 0;
            oldComment = "";
            navManager.NavigateTo("ideadescription/" + id, true);
        }

        /*
         * Logic for removing a comment from the database. This will also refresh the idea description page.
         */
        public void removeComment(int commentID)
        {
            _service.RemoveComment(commentID);
            navManager.NavigateTo("ideadescription/" + id, true);
        }

        // --------------------------- End of Methods Relating to Comments --------------------------

        // -------------------------- Methods Relating to Editing an Idea ---------------------------

        /*
         * Pops up the edit idea overlay. This overlay will allow the user to edit many values of an idea and possibly
         * close the current idea.
         */
        public void editIdea()
        {
            oldIdeaName = currentIdea.Name;
            oldIdeaType = currentIdea.Type;
            oldIdeaDescription = currentIdea.Description;
            oldIdeaURL = currentIdea.URL;
            // These objects are going to be used to edit the current values of the idea. This approach is very similar to
            // edit comment approach.

            updateVisible = true;
            editIdeaVisible = true;
        }

        /*
         * Pops up the update idea confirmation overlay which will be used to show the changes made to the idea before making
         * the changes.
         */
        public void updateIdeaConfirmation()
        {
            updateVisible = false;
            editIdeaVisible = false;
            editIdeaConfirmationVisible = true;
        }

        /*
         * Updates the current idea with the users provided changes.
         * Makes a database UPDATE call and uses NavManager to reload the page.
         */
        public void updateIdea()
        {
            _service.editIdea(currentIdea.RequestID, newIdeaNameField.Value,
                newIdeaTypeField.Value, newIdeaDescriptionField.Value, newIdeaURLField.Value);

            editIdeaConfirmationVisible = false;

            oldIdeaName = "";
            oldIdeaType = "";
            oldIdeaDescription = "";
            oldIdeaURL = "";

            navManager.NavigateTo("ideadescription/" + id, true);

        }

        /*
         * Closes the update idea overlay and returns the user back to the edit idea overlay.
         */
        public void cancelIdeaUpdate()
        {
            updateVisible = true;
            editIdeaVisible = true;
            editIdeaConfirmationVisible = false;
        }

        // ---------------------- End of Methods Relating to Editing an Idea ------------------------

        // --------------------------- Methods Relating to Closing an Idea --------------------------

        /*
         * Pops up the close idea overlay.
         */
        public void close()
        {
            editIdeaVisible = false;
            closeIdeaVisible = true;
        }

        /*
         * Hides the close idea overlay and brings the user back to the edit idea overlay
         */
        public void cancelClosure()
        {
            closeIdeaVisible = false;
            editIdeaVisible = true;
        }

        /*
         * Logic for closing an idea. This will redirect the user back to the idea list page.
         * Makes a database UPDATE call.
         */
        public void closeIdea()
        {
            _service.CloseIdea(currentIdea.RequestID, resolutionField.Value);
            closeIdeaVisible = false;
            navManager.NavigateTo("idealist");
        }

        // ---------------------- End of Methods Relating to Closing an Idea ------------------------

        // --------------------------- Methods Relating to Reopening Ideas --------------------------

        /*
         * Opens the reopen idea confirmation overlay which asks the user if they really want to reopen the current idea.
         */
        public void reopenIdea()
        {
            reopenIdeaConfirmationVisible = true;
        }

        /*
         * Reopens the current idea deleting whatever was in the resolution section.
         * This makes a database UPDATE call and uses NavManager to reload the page
         */
        public void reopenIdeaConfirmation()
        {
            _service.reopenIdea(currentIdea.RequestID);
            reopenIdeaConfirmationVisible = false;
            navManager.NavigateTo("ideadescription/" + id, true);
        }

        /*
         * Closes the reopen idea confirmation overlay.
         */
        public void cancelReopenIdea()
        {
            reopenIdeaConfirmationVisible = false;
        }

        // ----------------------- End of Methods Relating to Reopening Ideas -----------------------

        /*
         * This method is used for all update methods except for the reopen idea section.
         * If the user doesn't want to update a section of this idea this method is invoked closing any of the overlays.
         */
        public void cancelUpdate()
        {
            if (editIdeaVisible)
            {
                oldIdeaName = "";
                oldIdeaType = "";
                oldIdeaDescription = "";
                oldIdeaURL = "";
            }

            updateVisible = false;
            addCommentVisible = false;
            closeIdeaVisible = false;
            editCommentVisible = false;
            editIdeaVisible = false;
        }

        // ------------------------- End of Methods for IdeaDescription Logic -------------------------
    }
}
