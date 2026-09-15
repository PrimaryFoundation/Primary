public class CommentResponse
{
   public Guid Id { get; set;}
   public required string Text { get; set;}
   public required string AuthorName { get; set;}
   public DateTime CreatedAt { get; set;}
}