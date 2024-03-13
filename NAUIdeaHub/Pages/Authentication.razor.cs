using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Services;
using NAUCountryIdeaHub.Models;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class Authentication : ComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        // Allows us to change to a different page within the project.
        [Inject] private IIdeaHubService _service { get; set; }
        public IEnumerable<User> users { get; set; } = new List<User>();
        [Inject] private ILoggedUserService _loggedUser { get; set; }
        // Service that allows us to share who is logged in to the different pages

        public User loggedInUser;
        // Value to display the logged in users data

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
        public void updateCurrentUser()
        {
            _loggedUser.setUser(users.FirstOrDefault(x => x.Email.Equals(usernameField.Value) && x.Password.Equals(passwordField.Value)));
            // Sets the shared value in the service to whatever the user inputted

            loggedInUser = _loggedUser.getUser();
            // Updates the pages user value

            NavigationManager.NavigateTo("idealist");
            // This will route to the idealist page after a successful login
        }

    }
}