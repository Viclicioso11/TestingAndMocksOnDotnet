namespace Api.Configurations;

public class TwilioConfigurations
{
    public required string BaseUrl { get; set; }
    public required string AccountSid { get; set; }
    public required string AuthToken { get; set; }
    public required string From { get; set; }
}
