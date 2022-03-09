using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text playerInfoText;
    public Text playerTwoInfoText;
    public Text turnText;
    public Text chatText;
    public InputField messageInputField;

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
        Player attacked = GameController.instance.GetGamePlayers().First(p => p.GetPlayerData().id == obj.attackedUser.id);
        attacked.UpdatePlayerStateData(obj.attackedUser);

        GameController.instance.GetGameData().whoseTurnID = obj.nextPlayerID;

        if (GameController.instance.GetPlayerData().id != obj.playedByUser)
        {
            //TODO: "pokazati" koju kartu je protivnik odigrao
        }

        SetTexts();
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
        //TODO: ako nije Player Turn "blokirati" igranje poteza, ali dozvoliti chat
    }

    public void SkipTurn()
    {
        GameData gd = GameController.instance.GetGameData();
        gd.whoseTurnID = 12; //TODO: da ide preko player liste
        PlayerStateData psd = GameController.instance.GetPlayerStateData();
        psd.manaPoints += 1;

        GameController.instance.apiHelper.GetPlayersInGame(3015);

        //TODO: odigrati potez kao skip turn da bi se DB (BP me podseca na blackpink i uvek se zapitam sta je na sekundu) updateovala

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

   public void ClgButton()
   {
        int gameID = GameController.instance.GetGameData().id;
        int userID = GameController.instance.GetPlayerData().id;

        GameController.instance.GetPlayerStateData().manaPoints -= 2;

        GameController.instance.apiHelper.Turn(gameID, userID, 2, 4, 2, 4, 3);
   }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
