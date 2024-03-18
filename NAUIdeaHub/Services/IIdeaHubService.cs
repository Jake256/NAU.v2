using NAUCountryIdeaHub.Models;

namespace NAUCountryIdeaHub.Services
{
    public interface IIdeaHubService
    {
        public Task<IEnumerable<Request>> GetIdeasAsync();
        //public Task<IEnumerable<Request>> GetCompletedIdeasAsync();
        public Task<IEnumerable<User>> GetUsersAsync();

        public Task<IEnumerable<RequestActions>> GetActionsAsync(int pk);

        public void LikeIdea(int ideaPK, int userPK);

        public void AlterLike(int ideaPK, int userPK, int value);

        public void FavoriteIdea(int ideaPK, int userPK);

        public void AlterFavorite(int ideaPK, int userPK, int value);
    }
}
