using Microsoft.EntityFrameworkCore;
using Primary.Models;
using PrimaryServer.Data;
using PrimaryServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

builder.Services.ConfigureHttpJsonOptions(o =>
o.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

var app = builder.Build();

Guid dummy = Guid.Parse("11111111-1111-1111-1111-111111111111"); // фикс-юзер для теста, пока нет JWT (T5)

app.MapPost("/api/auth/register", (RegisterPayload payload, AppDbContext db) =>
{
    var user = new User(payload.Name, payload.Username) { Id = dummy, Name = payload.Name, Username = payload.Username };
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created("/api/auth/register", new { user.Id, user.Name, user.Username });
});

app.MapPost("/api/auth/login", (LoginPayload payload) => // Post - запрос к серверу на СОЗДАНИЕ. Get - на получение.
 {
     return Results.Ok(new { Token = "12345" });
 });

app.MapPost("/api/posts", async (CreatePostPayloads payload, AppDbContext db) =>
{
    var post = new Post() { AuthorId = dummy, Text = payload.PostText, CreatedAt = DateTime.UtcNow, Id = Guid.NewGuid() };
    db.Posts.Add(post);
    await db.SaveChangesAsync();
    return Results.Created("/api/posts", post);
});

app.MapGet("/api/ping", () =>
{
    return Results.Ok("pong"); //l
});
app.MapPost("/api/posts/{postsId}/comments", async (Guid postsId, CreateCommentRequest req, AppDbContext db) =>
{
    var comment = new Comment() { Id = Guid.NewGuid(), PostId = postsId, AuthorId = dummy, Text = req.Text, CreatedAt = DateTime.UtcNow }; // todo - AuthorId это JWT. отдельная ветка регистрации и логин
    db.Comments.Add(comment);
    await db.SaveChangesAsync();
    var returning = new CommentResponse() { Id = comment.Id, AuthorName = "dummy", Text = comment.Text, CreatedAt = DateTime.UtcNow };
    return Results.Created("/api/posts/{postsId}/comments", returning);
});
app.MapPost("/api/posts/{id}/reactions", async (Guid id, RequestReaction body, AppDbContext db) =>
{
    var reacted = db.Reactions.FirstOrDefault(r => r.PostId == id && r.UserId == dummy);
    if (reacted != null)
    {
        db.Reactions.Remove(reacted);
        await db.SaveChangesAsync();
    }
    else
    {
        var reaction = new Reaction() { Id = Guid.NewGuid(), PostId = id, UserId = dummy, Type = body.type, CreatedAt = DateTime.UtcNow };
        await db.Reactions.AddAsync(reaction);
        await db.SaveChangesAsync();
    }
    var count = db.Reactions.Count(r => r.PostId == id);
    var nowReacted = db.Reactions.Any(r => r.PostId == id && r.UserId == dummy);
    return Results.Ok(new { Count = count, Reacted = nowReacted });

});

app.MapHub<SocialHub>("/hubs/social");


app.Run();
