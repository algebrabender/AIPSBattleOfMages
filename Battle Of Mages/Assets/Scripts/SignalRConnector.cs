using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using Assets.Scripts;

public class SignalRConnector
{
    private HubConnection connection;
    public Action<ChatMessage> OnChatMessageReceived;
    public Action<Invite> OnInviteReceived;
    public Action<string> OnJoinMessageReceived;
    public Action<string> OnLeaveMessageReceived;
    public Action<string> OnTurnInfoReceived;

    public SignalRConnector()
    {
        connection = new HubConnectionBuilder()
        .WithUrl("https://localhost:5001/messageHub")
        .Build();
    }

    public async Task InitAsync()
    {

        connection.On<string, string>("ReceiveMessage", (message, username) =>
        {
            OnChatMessageReceived.Invoke(new ChatMessage(username, message));
        });

        connection.On<string>("SendMessageJoin", message =>
        {
            OnJoinMessageReceived.Invoke(message);
        });

        connection.On<string>("SendMessageLeave", message =>
        {
            OnLeaveMessageReceived.Invoke(message);
        });

        connection.On<Invite>("ReceivedInvite", invite =>
        {
            OnInviteReceived.Invoke(invite);
        });

        //TODO: connection on turn


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

    public async void LeaveApp(int userID)
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

    public async void JoinGame(int gameID)
    {
        try
        {
            await connection.InvokeAsync("JoinGame", gameID);
        }
        catch(Exception e)
        {
            UnityEngine.Debug.LogError($"Error {e.Message}");
        }
    }

}


