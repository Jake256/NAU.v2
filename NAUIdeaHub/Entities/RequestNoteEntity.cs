namespace NAUIdeaHub.Entities
{
    public class RequestNoteEntity
    {
        public int RequestNoteID { get; set; }
        public int RequestID { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int Author { get; set; }
    }
}
