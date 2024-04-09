using NAUIdeaHub.Entities;

namespace NAUIdeaHub.Repositories
{
    public interface IIdeaHubRepository
    {
        public Task<IEnumerable<RequestEntity>> GetIdeasAsync();
        //public Task<IEnumerable<RequestEntity>> GetCompletedIdeasAsync();
        public Task<IEnumerable<UserEntity>> GetUsersAsync();

        public Task<IEnumerable<RequestActionsEntity>> GetActionsAsync(int pk);

        public Task<IEnumerable<RequestNoteEntity>> GetNotesAsync(int requestPK);

        public void LikeIdea(int ideaPK, int userPK);

        public void AlterLike(int ideaPK, int userPK, int value);

        public void FavoriteIdea(int ideaPK, int userPK);

        public void AlterFavorite(int ideaPK, int userPK, int value);

        public void AddComment(int ideaID, string comment, int userID);

        public void RemoveComment(int commentID);

        public void CloseIdea(int ideaID, string resolution);

        public void editComment(int commentID, string newComment);

        public void editIdea(int ideaID, string newName, string newType, string newDescription, string newURL);
      
        public Task<IEnumerable<RequestActionsEntity>> GetAllActionsAsync();

        public Task<IEnumerable<RequestEntity>> GetRequestsByUserAsync(int userId);

    }
}
