using Microsoft.AspNetCore.Components;
using NAUCountryIdeaHub.Models;
using NAUCountryIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaList : ComponentBase
    {
        [Inject] private IIdeaHubService _service { get; set; }

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Ideas = await _service.GetIdeasAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
