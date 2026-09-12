namespace Primary.Models;

public class CreatePostPayload
{
    public Guid PostId {get; set;}
    public string PostText {get; set;}
    public DateTime PostedAt {get; set;}
    public required User Author {get; set;}

    public CreatePostPayload(User author, string postText)
    {
        PostId = Guid.NewGuid();
        Author = author;
        PostText = postText;
        PostedAt = DateTime.UtcNow;
    }
}