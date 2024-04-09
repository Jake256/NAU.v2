using NAUIdeaHub.Entities;
using NAUIdeaHub.Models;

namespace NAUIdeaHub.Services
{
    public interface IIdeaHubService
    {
        public Task<IEnumerable<Request>> GetIdeasAsync();
        //public Task<IEnumerable<Request>> GetCompletedIdeasAsync();
        public Task<IEnumerable<User>> GetUsersAsync();

        public Task<IEnumerable<RequestActions>> GetAllActionsAsync();

        public Task<IEnumerable<RequestActions>> GetActionsAsync(int pk);

        public Task<IEnumerable<RequestNote>> GetNotesAsync(int requestPK);

        public void LikeIdea(int ideaPK, int userPK);

        public void AlterLike(int ideaPK, int userPK, int value);

        public void FavoriteIdea(int ideaPK, int userPK);

        public void AlterFavorite(int ideaPK, int userPK, int value);

        public void AddComment(int ideaPK, string comment, int userPK);

        public void RemoveComment(int commentID);

        public void CloseIdea(int ideaID, string resolution);

        public void editComment(int commentID, string newComment);

        public void editIdea(int ideaID, string newName, string newType, string newDescription, string newURL);

        public Task<IEnumerable<Request>> GetRequestsByUserAsync(int userId);
    }
}
