namespace NAUCountryIdeaHub.Entities
{
    public class RequestVoteEntity
    {
        public int UserID { get; set; }
        public int RequestID { get; set; }
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }
    }
}
