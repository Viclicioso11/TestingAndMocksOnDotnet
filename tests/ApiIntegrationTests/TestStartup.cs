namespace ApiIntegrationTests;

[SetUpFixture]
public static class TestStartup
{
    public static CustomWebApplicationFactory Factory { get; private set; } = null!;
    public static HttpClient HttpClient { get; private set; } = null!;

    [OneTimeSetUp]
    public static void Setup()
    {
        Factory = new CustomWebApplicationFactory();
        HttpClient = Factory.CreateClient();
    }

    [OneTimeTearDown]
    public static void TearDown()
    {
        HttpClient.Dispose();
        Factory.Dispose();
    }
}
