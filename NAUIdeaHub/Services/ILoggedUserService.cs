using NAUCountryIdeaHub.Models;

/*
 * This class is for sharing the variable of who is logged in throughout the project.
 * This is the interface for the main LoggedUserService class.
 * This class will outline what is all needed in the LoggedUserService class.
 */

namespace NAUIdeaHub.Services
{
    public interface ILoggedUserService
    {
        public User getUser();

        public void setUser(User user);
    }
}
