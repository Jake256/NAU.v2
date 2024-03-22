using Microsoft.AspNetCore.Components;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Shared
{
    public partial class MainLayout
    {
        [Inject] ILoggedUserService _loggedUser {  get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }

        public void signOut()
        {
            _loggedUser.setUser(null);

            NavigationManager.NavigateTo("auth");

            username = null;
        }
    }
}
