﻿using Mapster;
using NAUIdeaHub.Models;
using NAUIdeaHub.Repositories;

namespace NAUIdeaHub.Services
{
    public class IdeaHubService : IIdeaHubService
    {
        //Private Members - Same as in the service but for our Repo
        private readonly IIdeaHubRepository _repository;

        //Constructor
        public IdeaHubService(IIdeaHubRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Request>> GetIdeasAsync()
        {
            var idea = await _repository.GetIdeasAsync();
            return idea.Adapt<IEnumerable<Request>>().ToList(); //This is where mapster is need
        }
        //public async Task<IEnumerable<Request>> GetCompletedIdeasAsync()
        //{
        //    var idea = await _repository.GetCompletedIdeasAsync();
        //    return idea.Adapt<IEnumerable<Request>>().ToList(); //This is where mapster is need
        //}

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var user = await _repository.GetUsersAsync();
            return user.Adapt<IEnumerable<User>>().ToList(); //This is where mapster is need
        }

        public async Task<IEnumerable<RequestActions>> GetActionsAsync(int pk)
        {
            var actions = await _repository.GetActionsAsync(pk);
            return actions.Adapt<IEnumerable<RequestActions>>().ToList();
        }

        public async Task<IEnumerable<RequestNote>> GetNotesAsync(int requestPK)
        {
            var notes = await _repository.GetNotesAsync(requestPK);
            return notes.Adapt<IEnumerable<RequestNote>>().ToList();
        }

        // ============================= Beginning of methods for idea description =============================

        public void LikeIdea(int ideaPK, int userPK)
        {
            _repository.LikeIdea(ideaPK, userPK);
        }

        public void AlterLike(int ideaPK, int userPK, int value)
        {
            _repository.AlterLike(ideaPK, userPK, value);
        }

        public void FavoriteIdea(int ideaPK, int userPK)
        {
            _repository.FavoriteIdea(ideaPK, userPK);
        }

        public void AlterFavorite(int ideaPK, int userPK, int value)
        {
            _repository.AlterFavorite(ideaPK, userPK, value);
        }

        // =============================== End of methods for idea description ===============================
    }
}
