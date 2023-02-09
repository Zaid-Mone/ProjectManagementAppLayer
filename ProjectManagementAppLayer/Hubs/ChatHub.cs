using Microsoft.AspNetCore.SignalR;
using ProjectManagementAppLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Hubs
{
    public class ChatHub:Hub
    {
        // for collect count of users inside the room
        public override Task OnConnectedAsync()
        {
            ConnectedUsers.myConnectedUsers.Add(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        // to send messages
        public async Task SendMessageSync(string username, string message, string sendAt)
        {
            await Clients.All.SendAsync("SendUserMessage", username, message, sendAt);
        }

        // to show the name of the user that enter the room
        public async Task JoinRoom(string user)
        {
            await Clients.Others.SendAsync("JoinRoom", user, "Joined The Room");
        }

        // to remove user id from the room
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            ConnectedUsers.myConnectedUsers.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

    }
}
