using System;
public class CreateCommentRequest
{
    public required string Text { get; set;}
    public Guid AuthorId { get; set;} 
}