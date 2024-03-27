using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public class LoggedUserService : ILoggedUserService
    {
        /*
         * Creates and initializes the ProtectedSessionStore object
         */
        public ProtectedSessionStorage protectedSessionStore { get; set; }
        public LoggedUserService(ProtectedSessionStorage protectedSessionStore)
        {
            this.protectedSessionStore = protectedSessionStore;
        }

        /*
         * This method will return the current user logged in. If no one has logged in, then it will return null.
         */
        public async Task<User> GetUserAsync()
        {
            var result = await protectedSessionStore.GetAsync<User>("AuthenticatedUser");
            return result.Success ? result.Value : null;
        }

        /*
         * This method will set the currently logged in user from the inputted values.
         */
        public async void SetUser(User user)
        {
            await protectedSessionStore.SetAsync("AuthenticatedUser", user);
        }
    }
}
