using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Primary.Models;
using PrimaryServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();

app.MapPost("/api/auth/login", (LoginPayload payload) =>
{
    return Results.Ok(new { Token = "12345" });
});
app.MapPost("/api/posts", (CreatePostPayload payload) =>
{
    return Results.Ok(new { Token = "12345" });
});

app.MapHub<SocialHub>("/hubs/social");

app.Run();
