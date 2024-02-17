namespace NAUCountryIdeaHub.Models
{
    public class RequestVote
    {
        public int UserID { get; set; }
        public int RequestID { get; set; }
        public bool UpVote { get; set; }
        public bool DownVote { get; set; }
    }
}
