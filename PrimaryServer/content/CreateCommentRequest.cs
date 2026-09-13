using System;
public class CreateCommentRequest
{
    public required string Text { get; set;}
    public Guid PostId { get; set;}
}