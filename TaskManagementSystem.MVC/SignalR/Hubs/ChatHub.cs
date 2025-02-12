using Microsoft.AspNetCore.SignalR;

namespace TaskManagementSystem.MVC.SignalR.Hubs;

public class ChatHub : Hub
{
    private static readonly List<string> _onlineUsers = new List<string>();

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public override async Task OnConnectedAsync()
    {
        var userName = Context.User?.Identity!.Name;
        _onlineUsers.Add(userName);
        await Clients.All.SendAsync("UserOnline", userName);
        await SendOnlineUsers();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userName = Context.User?.Identity!.Name;
        _onlineUsers.Remove(userName);
        await Clients.All.SendAsync("UserOffline", userName);
        await SendOnlineUsers();
    }

    private async Task SendOnlineUsers()
    {
        await Clients.All.SendAsync("ReceiveOnlineUsers", _onlineUsers);
    }
}
