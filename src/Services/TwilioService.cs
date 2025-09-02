using System.Text;
using Api.Configurations;
using Api.Interfaces;
using Microsoft.Extensions.Options;

namespace Api.Services;

public class TwilioService : ITwilioService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<TwilioConfigurations> _config;

    public TwilioService(IOptions<TwilioConfigurations> config,
        HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_config.Value.AccountSid}:{_config.Value.AuthToken}"))}");
    }
    public async Task SendSms(string message, string to)
    {
        var url = $"{_config.Value.BaseUrl}/Accounts/{_config.Value.AccountSid}/Messages.json";

        var request = new HttpRequestMessage(HttpMethod.Post, url);

        var collection = new List<KeyValuePair<string, string>>
        {
            new("To", to),
            new("From", _config.Value.From),
            new("Body", message)
        };

        request.Content = new FormUrlEncodedContent(collection);
        
        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to send SMS");
        }
    }
}
