using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace webapi.Communication
{
    public class MessageHub : Hub
    {
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

        public async Task<string> SendInvite(int userID, string inviteFrom)
        {
            await Clients.User("User" + userID).SendAsync("ReceiveInvite", inviteFrom + " has invited you to a game!");
            
            return "Received invite for Game";
        }

        public async Task SendGroupChatMessage(int gameID, string username, string message)
        {
            await Clients.Group("Game" + gameID).SendAsync("ReceiveMessage", username, message);
        }
    }
}