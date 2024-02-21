using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Services;
using NAUCountryIdeaHub.Models;

namespace NAUIdeaHub.Pages
{
    public partial class Authentication : ComponentBase
    {
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

    }
}
