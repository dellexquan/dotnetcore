using Microsoft.AspNetCore.SignalR;

namespace aspnetcore.signalr.server;

public class ChatRoomHub : Hub
{
    public Task SendPublicMessage(string message)
    {
        var connId = this.Context.ConnectionId;
        var msg = $"{connId} {DateTime.Now}: {message}";
        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }
}