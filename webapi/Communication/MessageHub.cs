using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace webapi.Communication
{
    public class MessageHub : Hub
    {
        public async Task<string> JoinApp(int userID)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "User" + userID);

            return "Joined app";
        }

        public async Task LeaveApp(int userID)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "User" + userID);
        }

        public async Task<string> JoinGameGroup(int gameID, string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Game" + gameID);

            await Clients.Group("Game" + gameID).SendAsync("SendMessageJoin", username + " has joined the game!");
            
            return "Joined group for Game" + gameID;
        }

        public async Task<string> LeaveGameGroup(int gameID, string username)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Game" + gameID);
            
            await Clients.Group("Game" + gameID).SendAsync("SendMessageLeave", username + " has left the game!");

            return "Left group for Game" + gameID;
        }

        public async Task SendGroupChatMessage(int gameID, string username, string message)
        {
            await Clients.Group("Game" + gameID).SendAsync("ReceiveMessage", username, message);
        }
    }
}