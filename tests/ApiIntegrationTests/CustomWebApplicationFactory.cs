using Api.Configurations;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ApiIntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var twilioContainer = new ContainerBuilder()
                .WithImage("mockoon-twilio-mocks:latest")
                .WithPortBinding(3000, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilInternalTcpPortIsAvailable(3000))
                .Build();

            twilioContainer.StartAsync().Wait();

            var twilioUri = new UriBuilder(Uri.UriSchemeHttp, twilioContainer.Hostname, twilioContainer.GetMappedPublicPort(3000)).Uri;

            services.Configure<TwilioConfigurations>(options =>
            {
                options.BaseUrl = $"{twilioUri}2010-04-01";
                options.AccountSid = "AFakeAccountSid";
                options.AuthToken = "AFakeAuthToken";
                options.From = "+1234567890";
            });
        });
    }
}
