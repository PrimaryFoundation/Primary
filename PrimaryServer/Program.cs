using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Primary.Models;
using PrimaryServer.Data;
using PrimaryServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.MapPost("/api/auth/login", (LoginPayload payload) =>
{
    return Results.Ok(new { Token = "12345" });
});
app.MapPost("/api/posts", (CreatePostPayloads payload) =>
{
    return Results.Ok(new { Token = "12345" });
});
app.MapGet("/api/ping", (NetworkPacket packet) =>{
    return Results.Ok(new {Token = "12345"} );
});
app.MapPost("/api/posts/{postsId}/comments", (// какой еще гуид если в тз уже существует) => )  // разбирайся
app.MapHub<SocialHub>("/hubs/social");

app.Run();
