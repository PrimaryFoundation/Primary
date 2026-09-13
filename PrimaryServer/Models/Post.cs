namespace Primary.Models;

public class Post
{
    public Guid Id { get; set; }
    public required Guid AuthorId { get; set; }
    public required string Text { get; set; }
    public string? MediaKey { get; set; }
    public DateTime CreatedAt { get; set; }
}
