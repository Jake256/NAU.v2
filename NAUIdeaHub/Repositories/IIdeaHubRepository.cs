using NAUCountryIdeaHub.Entities;

namespace NAUCountryIdeaHub.Repositories
{
    public interface IIdeaHubRepository
    {
        public Task<IEnumerable<RequestEntity>> GetIdeasAsync();
        //public Task<IEnumerable<RequestEntity>> GetCompletedIdeasAsync();
        public Task<IEnumerable<UserEntity>> GetUsersAsync();

        public Task<IEnumerable<RequestActionsEntity>> GetActionsAsync(int pk);

        public void LikeIdea(int ideaPK, int userPK);

        public void AlterLike(int ideaPK, int userPK, int value);

        public void FavoriteIdea(int ideaPK, int userPK);

        public void AlterFavorite(int ideaPK, int userPK, int value);

    }
}
