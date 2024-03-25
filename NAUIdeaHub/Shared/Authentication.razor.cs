using Microsoft.AspNetCore.Components;
using NAUIdeaHub.Services;
using NAUIdeaHub.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace NAUIdeaHub.Shared
{
    public partial class Authentication : LayoutComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        // Allows us to change to a different page within the project.
        [Inject] private IIdeaHubService _service { get; set; }
        public IEnumerable<User> users { get; set; } = new List<User>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                users = await _service.GetUsersAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /*
         * This method will update the LoggedUserService which will allow the other pages to access this data.
         */
        //public void updateCurrentUser()
        //{
        //    _loggedUser.setUser(users.FirstOrDefault(x => x.Email.Equals(usernameField.Value) && x.Password.Equals(passwordField.Value)));
        //    // Sets the shared value in the service to whatever the user inputted

        //    loggedInUser = _loggedUser.getUser();
        //    // Updates the pages user value

        //    //refreshes page
        //    NavigationManager.NavigateTo(NavigationManager.Uri, true);

        //}

        public async void SetAuthenticatedUser(User user)
        {
            await SetSessionAuthenticatedUser();

            //refreshes page
            NavigationManager.NavigateTo(NavigationManager.Uri, true);

        }

        private async Task SetSessionAuthenticatedUser()
        {
            
            await ProtectedSessionStore.SetAsync("AuthenticatedUser", users.FirstOrDefault(x => x.Email.Equals(usernameField.Value) && x.Password.Equals(passwordField.Value)));
        }

    }
}