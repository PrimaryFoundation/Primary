
namespace Primary.Models;
public class User
{
    public required Guid Id {get; set;}
    public required string Name {get; set;}
    public required string Username {get; set;}
    public DateTime createdAt {get; set;}
    public bool IsOnline {get; set;} 
    public UserStatus Status {get; set;}

    public User(string name, string username)
    {
        Id = Guid.NewGuid();
        Name = name;
        Username = username;
        createdAt = DateTime.UtcNow;
        IsOnline = true;
        Status = UserStatus.Online;
    }

}