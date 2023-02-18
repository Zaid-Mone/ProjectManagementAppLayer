using Microsoft.AspNetCore.SignalR;
using ProjectManagementAppLayer.Utility;
using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Hubs
{
    public class ChatHub:Hub
    {
        // for collect count of users inside the room

        // add users to the online
        public override async Task OnConnectedAsync()
        {
            // Add the client to the dictionary when they connect
            ConnectedUsers._connectedClients
                .TryAdd(Context.ConnectionId, Context.User.Identity.Name);
            // Call the UpdateOnlineUsers method to update the client-side list of online users
            await Clients.All.SendAsync("UpdateOnlineUsers", GetOnlineUsers());

            await base.OnConnectedAsync();
        }
        // remove users to the online
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Remove the client from the dictionary when they disconnect
            ConnectedUsers._connectedClients
                .TryRemove(Context.ConnectionId, out var username);
            // Call the UpdateOnlineUsers method to update the client-side list of online users
            await Clients.All.SendAsync("UpdateOnlineUsers", GetOnlineUsers());

            await base.OnDisconnectedAsync(exception);
        }
        // show online users
        public List<string> GetOnlineUsers()
        {
            // Get the list of connected clients
            var onlineUsers = ConnectedUsers._connectedClients
                .Select(x => x.Value)
                .Distinct()
                .ToList();
            return onlineUsers;
        }
        // to send messages
        public async Task SendMessageSync(string username, string message, string sendAt,string avatar)
        {
         await Clients.All.SendAsync("SendUserMessage", username, message, sendAt, avatar);
        }
        // to show the name of the user that enter the room
        public async Task JoinRoom(string user)
        {
            await Clients.Others.SendAsync("JoinRoom", user, "Joined The Room");
        }
        public async Task IsTypeing(string user)
        {
            await Clients.Others.SendAsync("isType", user);
        }

    }
}
