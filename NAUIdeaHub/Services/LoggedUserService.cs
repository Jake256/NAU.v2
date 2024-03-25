using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public class LoggedUserService : ILoggedUserService
    {
        public User loggedInUser;
        // This value will hold the current users data

        /*
         * This method will return the current user logged in. If no one has logged in, then it will return null.
         */
        public User getUser()
        {
            return loggedInUser;
        }

        /*
         * This method will mainly be used in Pages.Authentication.razor.cs
         * This method will set the currently logged in user from the inputted values.
         */
        public void setUser( User user)
        {
            loggedInUser = user;
        }
    }
}
