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
    public Text playerThreeInfoText = null;
    public Text playerFourInfoText = null;
    public Text turnText;
    public Text chatText;
    public InputField messageInputField;

    private void ChangeTurnText()
    {
        if (GameController.instance.CheckTurn())
            turnText.text = "Your Turn!";
        else
            turnText.text = "Player Two turn!"; //TODO: promeniti player two na player x
    }

    private void SetTexts()
    {
        GameData gd = GameController.instance.GetGameData();
        UserData ud = GameController.instance.GetPlayerData();
        PlayerStateData psd = GameController.instance.GetPlayerStateData();

        playerInfoText.text = ud.username.Replace("\"", "") + "#" + ud.tag.Replace("\"", "") +
                              "\nHealth Points: " + psd.healthPoints + "\nMana Points: " + psd.manaPoints;

        List<Player> players = GameController.instance.GetGamePlayers();

        for (int i = 1; i <= gd.numOfPlayers /*players.Count*/; i++)
        {
            //Player enemy = players[i];
            //UserData enemyData = enemy.GetPlayerData();
            //PlayerStateData enemyPSD = enemy.GetPlayerStateData();
            if (i == 1)
                playerTwoInfoText.text = "Username#tag\nHealth Points: " + 10 + "\nMana Points: " + 5;
            //playerInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
            //                   "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
            else if (i == 2)
                playerThreeInfoText.text = "Username#tag\nHealth Points: " + 10 + "\nMana Points: " + 5;
            //playerThreeInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
            //                   "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
            else
                playerFourInfoText.text = "Username#tag\nHealth Points: " + 10 + "\nMana Points: " + 5;
            //playerFourInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
            //                   "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
        }

        
    }

    private void UpdateChat(ChatMessageData obj)
    {
        var lastMessages = chatText.text;

        if (string.IsNullOrEmpty(lastMessages) == false)
            lastMessages += "\n";

        lastMessages += $"{obj.Username}: {obj.Message}";

        chatText.text = lastMessages;
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

        //Deal New Card
    }

    private void UpdateJoinLeave(ChatMessageData obj)
    {
        UpdateChat(obj);

        SetTexts();

        //Deal Cards
    }

    void Start()
    {
        GameController.instance.signalRConnector.OnChatMessageReceived += UpdateChat;
        GameController.instance.signalRConnector.OnJoinMessageReceived += UpdateJoinLeave;
        GameController.instance.signalRConnector.OnLeaveMessageReceived += UpdateJoinLeave;
        GameController.instance.signalRConnector.OnTurnInfoReceived += UpdateTurn;

        SetTexts();
        ChangeTurnText();

        //Deal Cards
    }

    void Update()
    {
        //TODO: ako nije Player Turn "blokirati" igranje poteza, ali dozvoliti chat

        if (GameController.instance.CheckTurn())
        {

        }
        else
        {

        }
    }

    public void SkipTurn()
    {
        GameData gd = GameController.instance.GetGameData();

        List<Player> players = GameController.instance.GetGamePlayers();

        int playerID = players.IndexOf(GameController.instance.GetPlayer());
        UserData nextPlayerData = players[(playerID + 1)%players.Count].GetPlayerData();
        //gd.whoseTurnID = nextPlayerData.id;
        gd.whoseTurnID = 12; 
        PlayerStateData psd = GameController.instance.GetPlayerStateData();
        psd.manaPoints += 1;

        //GameController.instance.apiHelper.GetPlayersInGame(3015);

        GameController.instance.apiHelper.SkipTurn(gd.id, GameController.instance.GetPlayerData().id, gd.whoseTurnID);

        GameController.instance.UpdateGameData(gd);
        GameController.instance.UpdatePlayerStateData(psd);

        SetTexts();
        ChangeTurnText();
    }

    public async void SendChatMessage()
    {
        int gameID = GameController.instance.GetGameData().id;
        UserData player = GameController.instance.GetPlayerData();
        string usernameWithTag = player.username.Replace("\"", "") + player.tag.Replace("\"", "");
        string message = messageInputField.text;

        await GameController.instance.signalRConnector.SendChatMessage(gameID, usernameWithTag, message);
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
