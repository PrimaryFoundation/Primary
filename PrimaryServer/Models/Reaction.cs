namespace Primary.Models;

public class Reaction
{
    public Guid Id { get; set; }
    public required Guid PostId { get; set; }
    public required Guid UserId { get; set; }
    public required ReactionType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}
