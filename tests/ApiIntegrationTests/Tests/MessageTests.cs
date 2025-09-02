using System.Net;
using System.Net.Http.Json;
using Api.Models;

namespace ApiIntegrationTests.Tests;

[TestFixture]
public class MessageTests
{
    private HttpClient client = new();
    
    [SetUp]
    public void Setup()
    {
        client = TestStartup.HttpClient;
    }

    [Test]
    public async Task SendMessage_WhenMessageIsSent_ShouldReturnSuccess()
    {
        var request = new MessageRequestModel
        {
            Message = "This thest should pass",
            To = "+19514004113"
        };

        var response = await client.PostAsJsonAsync("/api/v1/messages", request);
        TestContext.Out.WriteLine(await response.Content.ReadAsStringAsync());

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task SendMessage_WhenMessageIsNotSent_ShouldReturnError()
    {
        var request = new MessageRequestModel
        {
            Message = "This test should pass when an error occurs",
            To = "+1838383838" // The phone number that the mock will respond with a 400 error
        };

        var response = await client.PostAsJsonAsync("/api/v1/messages", request);

        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
    }
}
