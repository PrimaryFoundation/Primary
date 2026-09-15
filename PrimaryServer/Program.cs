using Microsoft.EntityFrameworkCore;
using Primary.Models;
using PrimaryServer.Data;
using PrimaryServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.MapPost("/api/auth/login", (LoginPayload payload) => // Post - запрос к серверу на СОЗДАНИЕ. Get - на получение.
 {
     return Results.Ok(new { Token = "12345" });
 });

app.MapPost("/api/posts", async (CreatePostPayloads payload, AppDbContext db) =>
{
    Guid author = Guid.NewGuid();
    var post = new Post() { AuthorId = author, Text = payload.PostText, CreatedAt = DateTime.UtcNow, Id = Guid.NewGuid() };
    db.Posts.Add(post);
    await db.SaveChangesAsync();
    return Results.Created("/api/posts", post);
});

app.MapGet("/api/ping", (string d) =>
{
    return Results.Ok(new { Token = "12345" });
});
app.MapPost("/api/posts/{postsId}/comments", async (Guid postsId, CreateCommentRequest req, AppDbContext db) =>
{
    Guid TESTdummy = Guid.NewGuid();
    var comment = new Comment() { Id = Guid.NewGuid(), PostId = postsId, AuthorId = TESTdummy, Text = req.Text, CreatedAt = DateTime.UtcNow }; // todo - AuthorId это JWT. отдельная ветка регистрации и логин
    db.Comments.Add(comment);
    await db.SaveChangesAsync();
    var returning = new CommentResponse() { Id = comment.Id, AuthorName = "dummy", Text = comment.Text, CreatedAt = DateTime.UtcNow };
    return Results.Created("/api/posts/{postsId}/comments", returning);
});
app.MapPost("/api/posts/{id}/reactions", async (Guid id, RequestReaction body, AppDbContext db) =>
{
    Guid dummy = Guid.NewGuid();
    var reacted = db.Reactions.FirstOrDefault(r => r.PostId == id && r.UserId == dummy) ?? null;
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
    return Results.Ok(new { Count = count, Reacted = reacted != null });

});

app.MapHub<SocialHub>("/hubs/social");


app.Run();
