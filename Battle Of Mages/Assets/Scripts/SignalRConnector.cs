using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

public class SignalRConnector
{
    private HubConnection connection;
    public Action<ChatMessageData> OnChatMessageReceived;
    public Action<InviteData> OnInviteReceived;
    public Action<ChatMessageData> OnJoinMessageReceived;
    public Action<UserData> OnJoinUpdateReceived;
    public Action<ChatMessageData> OnLeaveMessageReceived;
    public Action<TurnData> OnTurnInfoReceived;
    public Action<UserData, PlayerStateData> OnPlayersChangesReceived;
    public Action<string> OnEndGame;
    public Action<int, GameData> OnRemoveUserFromGame;

    public SignalRConnector()
    {
        connection = new HubConnectionBuilder()
        .WithUrl("https://localhost:5001/messageHub")
        .ConfigureLogging(logging =>
        {
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Debug);
        })
        .Build();

        connection.KeepAliveInterval = TimeSpan.FromSeconds(15);
        connection.ServerTimeout = TimeSpan.FromMinutes(5);
    }

    public async Task InitAsync()
    {

        connection.On<string, string>("ReceiveMessage", (username, message) =>
        {
            OnChatMessageReceived.Invoke(new ChatMessageData
            {
                Username = username,
                Message = message
            });
        });

        connection.On<string, string>("SendMessageJoin", (username, message) =>
        {
            OnJoinMessageReceived.Invoke(new ChatMessageData
            {
                Username = username,
                Message = message
            });
        });

        connection.On<string, string> ("SendMessageLeave", (username, message) =>
        {
            OnLeaveMessageReceived.Invoke(new ChatMessageData
            {
                Username = username,
                Message = message
            });
        });

        connection.On<InviteData>("ReceivedInvite", invite =>
        {
            OnInviteReceived.Invoke(invite);
        });

        connection.On<TurnData>("Turn", turn =>
        {
            OnTurnInfoReceived.Invoke(turn);
        });

        connection.On<UserData, PlayerStateData>("AddUserToGame", (user, playerState) =>
        {
            OnPlayersChangesReceived.Invoke(user, playerState);
        });

        connection.On<string>("EndGame", msg =>
        {
            OnEndGame.Invoke(msg);
        });

        connection.On<int, GameData>("RemoveUserFromGame", (userID, game) =>
        {
            OnRemoveUserFromGame.Invoke(userID, game);
        });

        await this.StartConnectionAsync();
    }

    private async Task StartConnectionAsync()
    {
        try
        {
            await connection.StartAsync();
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }

    public async Task JoinApp(int userID)
    {
        try
        {
            await connection.InvokeAsync("JoinApp", userID);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Error {e.Message}");
        }
    }

    public async Task LeaveApp(int userID)
    {
        try
        {
            await connection.InvokeAsync("LeaveApp", userID);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Error {e.Message}");
        }
    }

    public async Task JoinGame(int gameID, string username)
    {
        try
        {
            await connection.InvokeAsync("JoinGameGroup", gameID, username);
        }
        catch(Exception e)
        {
            UnityEngine.Debug.LogError($"Error {e.Message}");
        }
    }

    public async Task LeaveGame(int gameID, string username)
    {
        try
        {
            await connection.InvokeAsync("LeaveGameGroup", gameID, username);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Error {e.Message}");
        }
    }

    public async Task SendChatMessage(int gameID, string username, string message)
    {
        try
        {
            await connection.InvokeAsync("SendGroupChatMessage", gameID, username, message);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"Error {e.Message}");
        }
    }
}


