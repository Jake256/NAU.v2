namespace NAUCountryIdeaHub.Models
{
    public class Request
    {
        public int RequestID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Closed { get; set; }
        public string Description { get; set; }
        public string URL {  get; set; }
        public string Resolution { get; set; }
        public DateTime DateTimeSubmitted { get; set; }
        public DateTime DateTimeResolved { get; set; }
        public int Requestor { get; set; }
        public int RequestAdmin { get; set; }
    }
}
