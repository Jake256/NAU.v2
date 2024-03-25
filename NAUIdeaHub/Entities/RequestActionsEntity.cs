namespace NAUIdeaHub.Entities
{
    public class RequestActionsEntity
    {
        public int UserID { get; set; }
        public int RequestID { get; set; }
        public bool UpVote { get; set; }
        public bool Favorite { get; set; }
    }
}
