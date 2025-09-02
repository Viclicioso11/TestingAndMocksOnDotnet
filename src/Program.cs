using Api.Configurations;
using Api.Interfaces;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.Configure<TwilioConfigurations>(builder.Configuration.GetSection("TwilioService"));

builder.Services.AddHttpClient<ITwilioService, TwilioService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/v1/messages", async (
    [FromBody] MessageRequestModel messageRequestModel,
    [FromServices] ITwilioService twilioService) =>
{
    try
    {
        await twilioService.SendSms(messageRequestModel.Message, messageRequestModel.To);
        return Results.Ok(new { message = "Message sent successfully" });
    }
    catch (Exception ex)
    {
        return Results.InternalServerError(new { message = ex.Message}); // Just for POC purposes
    }
}).WithName("SendMessage");

app.Run();

// Required for WebApplicationFactory in tests
public partial class Program { }
