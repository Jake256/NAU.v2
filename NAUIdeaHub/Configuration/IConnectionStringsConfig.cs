namespace NAUIdeaHub.Configuration
{
    public interface IConnectionStringsConfig
    {
        string DefaultConnection { get; set; }
        string SendGridApiKey   { get; set; }
    }
}
