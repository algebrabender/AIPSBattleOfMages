using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

public class DemoScene : MonoBehaviour
{
    public InputField gameIDInputField;
    public InputField messageInputField;
    public InputField userNameEntryField;
    public Text chatMessagesText;
    public Text invitesText;
    public Text groupUpdatesText;
    public Text turnText;

    public void Start()
    {
        GameController.instance.signalRConnector.OnChatMessageReceived += UpdateChat;
        GameController.instance.signalRConnector.OnInviteReceived += UpdateInvites;
        GameController.instance.signalRConnector.OnJoinMessageReceived += UpdateJoinMessage;
        GameController.instance.signalRConnector.OnLeaveMessageReceived += UpdateJoinMessage;
        GameController.instance.signalRConnector.OnTurnInfoReceived += UpdateTurnMessage;
    }

    private void UpdateChat(ChatMessage obj)
    {
        var lastMessages = this.chatMessagesText.text;

        if (string.IsNullOrEmpty(lastMessages) == false)
            lastMessages += "\n";

        lastMessages += $"{obj.Username}: {obj.Message}";
        this.chatMessagesText.text = lastMessages;
    }

    private void UpdateInvites(Invite obj)
    {
        var lastInvites = this.invitesText.text;

        if (string.IsNullOrEmpty(lastInvites) == false)
            lastInvites += "\n";

        lastInvites += $"User {obj.UserFrom} invited you to Game with ID: {obj.GameID}";
        this.invitesText.text = lastInvites;
    }

    private void UpdateJoinMessage(string obj)
    {
        var lastGroupUpdates = this.groupUpdatesText.text;

        if (string.IsNullOrEmpty (lastGroupUpdates) == false)
            lastGroupUpdates += "\n";

        lastGroupUpdates += obj;
        this.groupUpdatesText.text = lastGroupUpdates;
    }

    private void UpdateTurnMessage(string obj)
    {
        this.turnText.text = obj;
    }

    public async void JoinGame()
    {
        int gameID;
        Int32.TryParse(gameIDInputField.text, out gameID);

        string username = userNameEntryField.text;

        await GameController.instance.signalRConnector.JoinGame(gameID, username);
    }

    public async void LeaveGame()
    {
        int gameID;
        Int32.TryParse(gameIDInputField.text, out gameID);

        string username = userNameEntryField.text;

        await GameController.instance.signalRConnector.LeaveGame(gameID, username);
    }

    public async void SendChatMessage()
    {
        int gameID;
        Int32.TryParse(gameIDInputField.text, out gameID);

        string username = userNameEntryField.text;
        string message = messageInputField.text;

        await GameController.instance.signalRConnector.SendChatMessage(gameID, username, message);
    }
}
