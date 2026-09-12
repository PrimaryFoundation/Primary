namespace Primary.Models;

public class LoginPayload
{
    public string Username { get; set; }
    public string Password { get; set; }

    public LoginPayload(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
public class RegisterPayload
{
    public Guid id {get; set;} = Guid.NewGuid();
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PublicKey {get; set;}

    public RegisterPayload(string name, string username, string password, string publickey)
    {
        Name = name;
        Username = username;
        Password = password;
        PublicKey = publickey;
    }
}