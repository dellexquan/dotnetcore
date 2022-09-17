using System.Security.Claims;
using aspnetcore.identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace aspnetcore.signalr.server;

[Authorize]
public class ChatRoomHub : Hub
{
    private readonly UserManager<MyUser> userManager;

    public ChatRoomHub(UserManager<MyUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task SendPrivateMessage(string toUserName, string message)
    {
        var user = await userManager.FindByNameAsync(toUserName);
        var userId = user.Id;
        var fromUserName = this.Context.User!.FindFirst(ClaimTypes.Name)!.Value;
        var msg = message;
        await this.Clients.User(userId.ToString()).SendAsync("ReceivePrivateMessage", fromUserName, msg);
    }

    public Task SendPublicMessage(string message)
    {
        var userNameClaim = this.Context.User!.FindFirst(ClaimTypes.Name);

        var connId = this.Context.ConnectionId;
        var msg = $"{connId} {userNameClaim!.Value} {DateTime.Now}: {message}";
        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }

    // public override Task OnConnectedAsync()
    // {
    //     var context = this.Context.GetHttpContext();
    //     var token = context!.Request.Query["access_token"];
    //     if (string.IsNullOrEmpty(token))
    //     {
    //         return Task.CompletedTask;
    //     }
    //     return base.OnConnectedAsync();
    // }
}