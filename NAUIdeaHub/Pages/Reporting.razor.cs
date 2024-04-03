using Microsoft.AspNetCore.Components;
using NAUIdeaHub.Models;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class Reporting : ComponentBase
    {
        [Inject] private IIdeaHubService _service { get; set; }
        [Inject] private NavigationManager navigationManager { get; set; }
        [Inject] private ILoggedUserService protectedSessionStore { get; set; }

        public User? authenticatedUser;
        public IEnumerable<Request> requests { get; set; } = new List<Request>();

        protected override async Task OnInitializedAsync()
        {
            authenticatedUser = await protectedSessionStore.GetUserAsync();

            requests = await _service.GetRequestsByUserAsync(authenticatedUser.UserID);

        }

        public void GoToRequestDetails(int requestId)
        {
            navigationManager.NavigateTo("ideadescription/" + requestId);
        }

    }
}
