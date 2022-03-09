using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text playerInfoText;
    public Text playerTwoInfoText;
    public Text turnText;
    public Text chatText;
    public InputField messageInputField;

    private void UpdateChat(ChatMessageData obj)
    {
        var lastMessages = chatText.text;

        if (string.IsNullOrEmpty(lastMessages) == false)
            lastMessages += "\n";

        lastMessages += $"{obj.Username}: {obj.Message}";

        chatText.text = lastMessages; //TODO: videti zasto ne updatuje text polje
    }

    private void UpdateTurn(TurnData obj)
    {

    }

    private void ChangeTurnText()
    {
        if (GameController.instance.CheckTurn())
            turnText.text = "Your Turn!";
        else
            turnText.text = "Player Two turn!";
    }

    private void SetTexts()
    {
        UserData ud = GameController.instance.GetPlayerData();
        PlayerStateData psd = GameController.instance.GetPlayerStateData();

        playerInfoText.text = ud.username.Replace("\"", "") + "#" + ud.tag.Replace("\"", "") +
                              "\nHealth Points: " + psd.healthPoints + "\nMana Points: " + psd.manaPoints;

        //TODO: ovo preko player list
        playerTwoInfoText.text = "Username#tag\nHealth Points: " + 10 + "\nMana Points: " + 5;
    }

    void Start()
    {
        GameController.instance.signalRConnector.OnChatMessageReceived += UpdateChat;
        GameController.instance.signalRConnector.OnJoinMessageReceived += UpdateChat;
        GameController.instance.signalRConnector.OnLeaveMessageReceived += UpdateChat;
        GameController.instance.signalRConnector.OnTurnInfoReceived += UpdateTurn;
        SetTexts();
        ChangeTurnText();
    }

    void Update()
    {
           
    }

    public void SkipTurn()
    {
        GameData gd = GameController.instance.GetGameData();
        gd.whoseTurnID = 12; //TODO: da ide preko player liste
        PlayerStateData psd = GameController.instance.GetPlayerStateData();
        psd.manaPoints += 1;

        //TODO: odigrati potez kao skip turn da bi se BP updateovala

        GameController.instance.UpdateGameData(gd);
        GameController.instance.UpdatePlayerStateData(psd);

        SetTexts();
        ChangeTurnText();
    }

    public async void SendChatMessage()
    {
        int gameID = GameController.instance.GetGameData().id;
        UserData player = GameController.instance.GetPlayerData();
        string username = player.username.Replace("\"", "");
        string message = messageInputField.text;

        await GameController.instance.signalRConnector.SendChatMessage(gameID, username, message);
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
