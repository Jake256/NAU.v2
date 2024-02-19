﻿using NAUCountryIdeaHub.Entities;

namespace NAUCountryIdeaHub.Repositories
{
    public interface IIdeaHubRepository
    {
        //-------------Example----------------------------------
        public Task<IEnumerable<RequestEntity>> GetIdeasAsync();
        public Task<IEnumerable<RequestEntity>> GetCompletedIdeasAsync();
        //------------------------------------------------------
    }
}
