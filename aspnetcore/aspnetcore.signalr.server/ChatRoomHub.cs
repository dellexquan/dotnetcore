using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace aspnetcore.signalr.server;

[Authorize]
public class ChatRoomHub : Hub
{
    public Task SendPublicMessage(string message)
    {
        var userNameClaim = this.Context.User!.FindFirst(ClaimTypes.Name);

        var connId = this.Context.ConnectionId;
        var msg = $"{connId} {userNameClaim!.Value} {DateTime.Now}: {message}";
        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }

    public override Task OnConnectedAsync()
    {
        var context = this.Context.GetHttpContext();
        var token = context!.Request.Query["access_token"];
        if (string.IsNullOrEmpty(token))
        {
            return Task.CompletedTask;
        }
        return base.OnConnectedAsync();
    }
}