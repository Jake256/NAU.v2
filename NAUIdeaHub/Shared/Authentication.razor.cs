using Microsoft.AspNetCore.Components;
using NAUIdeaHub.Services;
using NAUIdeaHub.Models;

namespace NAUIdeaHub.Shared
{
    public partial class Authentication : LayoutComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IIdeaHubService _service { get; set; }
        [Inject] private ILoggedUserService protectedSessionStore { get; set; }
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

        public async void SetAuthenticatedUser(User user)
        {
            await SetSessionAuthenticatedUser(user);

            //refreshes page
            NavigationManager.NavigateTo(NavigationManager.Uri, true);

        }

        private async Task SetSessionAuthenticatedUser(User user)
        {
            User loggedInUser = user;
            protectedSessionStore.SetUser(loggedInUser);
            await ProtectedSessionStore.SetAsync("AuthenticatedUser", loggedInUser);
        }

    }
}