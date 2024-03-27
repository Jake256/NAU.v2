using NAUIdeaHub.Models;

/*
 * This class is for sharing the ProtectedSessionStorage which stores the user object of the signed in user.
 * This is the interface for the LoggedUserService class.
 */

namespace NAUIdeaHub.Services
{
    public interface ILoggedUserService
    {
        public Task<User> GetUserAsync();

        public void SetUser(User user);
    }
}
