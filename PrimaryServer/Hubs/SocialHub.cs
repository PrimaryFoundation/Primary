using Microsoft.AspNetCore.SignalR;

namespace PrimaryServer.Hubs;

public class SocialHub : Hub
{
    public async Task SendDirectMessage(Guid receiverId, string encryptedPayload)
    {
        await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", encryptedPayload);
    }
}
