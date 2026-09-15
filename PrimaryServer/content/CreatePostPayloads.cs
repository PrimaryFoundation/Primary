namespace Primary.Models;

public class CreatePostPayloads
{
    public Guid PostId {get; set;}
    public string PostText {get; set;}
    public DateTime PostedAt {get; set;}
    
    public CreatePostPayloads(string postText)
    {
        PostId = Guid.NewGuid();
        PostText = postText;
        PostedAt = DateTime.UtcNow;
    }
}