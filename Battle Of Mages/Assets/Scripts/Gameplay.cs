using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Image terrainImage;
    public Sprite fireTerrain;
    public Sprite iceTerrain;
    public Sprite earthTerrain;
    public Sprite airTerrain;

    private void ChangeTurnText()
    {
        turnText.text = GameController.instance.CheckTurn() + " is playing!";
    }

    private void SetTexts()
    {
        GameData gd = GameController.instance.GetGameData();
        UserData ud = GameController.instance.GetPlayerData();
        PlayerStateData psd = GameController.instance.GetPlayerStateData();

        playerInfoText.text = ud.username.Replace("\"", "") + "#" + ud.tag.Replace("\"", "") +
                              "\nHealth Points: " + psd.healthPoints + "\nMana Points: " + psd.manaPoints;

        List<Player> players = GameController.instance.GetGamePlayers();

        for (int i = 0; i < players.Count; i++)
        {
            Player enemy = players[i];
            UserData enemyData = enemy.GetPlayerData();
            PlayerStateData enemyPSD = enemy.GetPlayerStateData();
            if (i + 1 == 1)
                playerTwoInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
                                        "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
            else if (i + 1 == 2)
                playerThreeInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
                                          "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
            else
                playerFourInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
                                         "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
        }
    }

    private void UpdateChat(ChatMessageData obj)
    {
        //var lastMessages = chatText.text;

        //if (string.IsNullOrEmpty(lastMessages) == false)
        //    lastMessages += "\n";

        string lastMessages = $"{obj.Username}: {obj.Message}";

        GameController.instance.chatHistory += "\n" + lastMessages;

        chatText.text = GameController.instance.chatHistory;
    }

    private void UpdateTurn(TurnData obj)
    {
        GameController.instance.GetGameData().whoseTurnID = obj.nextPlayerID;

        if (obj.attackedUser != null) //odigrana karta else je skip turn kad ne treba nista
        {
            List<Player> players = GameController.instance.GetGamePlayers();
            Player attacked = players.Find(p => p.GetPlayerData().id == obj.attackedUser.id);
            attacked.UpdatePlayerStateData(obj.attackedUser);

            if (GameController.instance.GetPlayerData().id != obj.playedByUser)
            {
                Player player = players.Find(p => p.GetPlayerData().id == obj.attackedUser.id);
                //TODO: "pokazati" koju kartu je protivnik odigrao

                //TODO: izabrana karta
                //player.RemoveCard(card);
                player.DealCard();
            }
            else
            {
                //TODO: izabrana karta
                //player.RemoveCard(card);
                GameController.instance.GetPlayer().DealCard();
            }
        }

        SetTexts(); 
    }

    private void UpdateJoin(ChatMessageData obj)
    {
        StartCoroutine(GameController.instance.apiHelper.GetGamePlayers(GameController.instance.GetGameData().id));

        UpdateChat(obj);

        SetTexts();

        List<Player> players = GameController.instance.GetGamePlayers();
        foreach (Player player in players)
        {
            player.DealHand();
        }
    }

    private void UpdateLeave(ChatMessageData obj)
    {
        UpdateChat(obj);
    }

    void Awake()
    {
        GameData gameData = GameController.instance.GetGameData();
        StartCoroutine(GameController.instance.apiHelper.GetGamePlayers(gameData.id));
    }

    void Start()
    {
        GameController.instance.signalRConnector.OnChatMessageReceived += UpdateChat;
        GameController.instance.signalRConnector.OnJoinMessageReceived += UpdateJoin;
        GameController.instance.signalRConnector.OnLeaveMessageReceived += UpdateLeave;
        GameController.instance.signalRConnector.OnTurnInfoReceived += UpdateTurn;

        chatText.text = GameController.instance.chatHistory;

        if (terrainImage.sprite == null)
        {
            switch(GameController.instance.GetGameTerrain())
            {
                case "fire":
                    terrainImage.sprite = fireTerrain;
                    break;
                case "ice":
                    terrainImage.sprite = iceTerrain;
                    break;
                case "earth":
                    terrainImage.sprite = earthTerrain;
                    break;
                case "air":
                    terrainImage.sprite = airTerrain;
                    break;
            }
        }

        SetTexts();
        ChangeTurnText();

        GameController.instance.GetPlayer().DealHand();
    }

    void Update()
    {
        Button skipButton = GameObject.Find("SkipButton").GetComponent<Button>();

        if (GameController.instance.GetGameData().whoseTurnID != GameController.instance.GetPlayerData().id)
        {
            skipButton.interactable = false;

            //TODO: blokirati cards

            ChangeTurnText();
        }
        else
        {
            skipButton.interactable = true;

            //TODO: odblokirati karte
            ChangeTurnText();
        }

        chatText.text = GameController.instance.chatHistory;

        if (playerTwoInfoText.text == "" || playerThreeInfoText.text == "" || playerFourInfoText.text == "")
            SetTexts();
    }

    public void SkipTurn()
    {
        GameData gd = GameController.instance.GetGameData();

        List<Player> players = GameController.instance.GetGamePlayers();

        UserData nextPlayerData = players[0].GetPlayerData();
        gd.whoseTurnID = nextPlayerData.id;
        PlayerStateData psd = GameController.instance.GetPlayerStateData();
        psd.manaPoints += 1;

        StartCoroutine(GameController.instance.apiHelper.SkipTurn(gd.id, GameController.instance.GetPlayerData().id, gd.whoseTurnID));

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

    public void Quit()
    {
        GameController.instance.Quit();
    }

    public void SendInvites()
    {
        SceneManager.LoadScene(9);
    }
}
