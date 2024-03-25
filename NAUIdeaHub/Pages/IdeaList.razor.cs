using Microsoft.AspNetCore.Components;
using NAUIdeaHub.Models;
using NAUIdeaHub.Services;

namespace NAUIdeaHub.Pages
{
    public partial class IdeaList : ComponentBase
    {
        [Inject] private IIdeaHubService _service { get; set; }

        public IEnumerable<Request> Ideas { get; set; } = new List<Request>();

        public List<Request> CompletedIdeas { get; set; } = new List<Request>();

        protected override async Task OnInitializedAsync()
        {
            await GetAuthenticatedUser();

            try
            {
                Ideas = await _service.GetIdeasAsync();

                //CompletedIdeas = await _service.GetCompletedIdeasAsync(); 
                foreach (var x in Ideas)
                {
                    if (x.Closed == true)
                    {
                        CompletedIdeas.Add(x);
                    }
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

}
