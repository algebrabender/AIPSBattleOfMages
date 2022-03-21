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
    public Text playerTwoInfoText = null;
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
    public List<Card> playerHand;
    public List<Card> playerTwoHand;
    public List<Card> playerThreeHand;
    public List<Card> playerFourHand;
    public Player playerTwo;
    public Player playerThree;
    public Player playerFour;
    private bool cardsSet = true;

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
            {
                playerTwoInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
                                        "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
                playerTwo.SetPlayer(enemyData, enemyPSD);
            }
            else if (i + 1 == 2)
            {
                playerThreeInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
                                          "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
                playerThree.SetPlayer(enemyData, enemyPSD);
            }
            else
            {
                playerFourInfoText.text = enemyData.username.Replace("\"", "") + "#" + enemyData.tag.Replace("\"", "") +
                                         "\nHealth Points: " + enemyPSD.healthPoints + "\nMana Points: " + enemyPSD.manaPoints;
                playerFour.SetPlayer(enemyData, enemyPSD);
            }
        }
    }

    private void DealHandForPlayers()
    {
        List<Player> players = GameController.instance.GetGamePlayers();

        for (int i = 0; i < players.Count; i++)
        {
            if (i == 0)
                players[i].DealHand(playerTwoHand);
            else if (i == 1)
                players[i].DealHand(playerThreeHand);
            else
                players[i].DealHand(playerFourHand);
        }

        cardsSet = true;
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
        List<Player> players = GameController.instance.GetGamePlayers();

        if (obj.attackedUser != null) //odigrana karta else je skip turn kad ne treba nista
        {
            Player attacked = players.Find(p => p.GetPlayerData().id == obj.attackedUser.id);
            attacked.UpdatePlayerStateData(obj.attackedUser);

            if (GameController.instance.GetPlayerData().id != obj.playedByUser)
            {
                Player player = players.Find(p => p.GetPlayerData().id == obj.attackedUser.id);

                //TODO: proveriti da li radi kako treba
                if (playerTwo.GetPlayerData() == player.GetPlayerData())
                {
                    Card card = playerTwoHand.FirstOrDefault(c => c.cardData.id == obj.card.id);
                    card.highlightImage.enabled = true;
                    playerTwoHand.Remove(card);
                    player.DealHand(playerTwoHand);
                    card.highlightImage.enabled = false;
                }
                else if (playerThree.GetPlayerData() == player.GetPlayerData())
                {
                    Card card = playerTwoHand.FirstOrDefault(c => c.cardData.id == obj.card.id);
                    card.highlightImage.enabled = true;
                    playerThreeHand.Remove(card);
                    player.DealHand(playerThreeHand);
                    card.highlightImage.enabled = false;
                }
                else if (playerFour.GetPlayerData() == player.GetPlayerData())
                {
                    Card card = playerTwoHand.FirstOrDefault(c => c.cardData.id == obj.card.id);
                    card.highlightImage.enabled = true;
                    playerFourHand.Remove(card);
                    player.DealHand(playerFourHand);
                    card.highlightImage.enabled = false;
                }
            }
        }

        SetTexts(); 
    }

    private void UpdateJoin(ChatMessageData obj)
    {
        StartCoroutine(GameController.instance.apiHelper.GetGamePlayers(GameController.instance.GetGameData().id));

        UpdateChat(obj);

        SetTexts();

        GameData gd = GameController.instance.GetGameData();
        List<Player> players = GameController.instance.GetGamePlayers();

        if (gd.numOfPlayers == players.Count)
        {
            foreach (Player player in players)
            {
                StartCoroutine(GameController.instance.apiHelper.GetDeckWithCards(player.GetPlayerStateData().id, player.GetPlayerData().id));
            }

            cardsSet = false;
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

        GameController.instance.GetPlayer().DealHand(playerHand);

        List<Player> players = GameController.instance.GetGamePlayers();
        foreach (Player player in players)
        {
            StartCoroutine(GameController.instance.apiHelper.GetDeckWithCards(player.GetPlayerStateData().deckID, player.GetPlayerData().id));
        }

        if (players.Count == GameController.instance.GetGameData().numOfPlayers - 1)
            DealHandForPlayers();
    }

    void Update()
    {
        if (playerTwoInfoText.text == "" || playerThreeInfoText.text == "" || playerFourInfoText.text == "")
            SetTexts();

        if (!cardsSet)
            DealHandForPlayers();

        Button skipButton = GameObject.Find("SkipButton").GetComponent<Button>();

        if (GameController.instance.GetGameData().whoseTurnID != GameController.instance.GetPlayerData().id)
        {
            skipButton.interactable = false;

            ChangeTurnText();
        }
        else
        {
            skipButton.interactable = true;

            ChangeTurnText();

            foreach (Card card in playerHand)
            {
                if (card.clicked)
                {
                    Player player = GameController.instance.GetGamePlayers().FirstOrDefault(p => p.clicked == true);

                    if (player.clicked)
                    {
                        card.clicked = false;

                        playerHand.Remove(card);

                        player.clicked = false;

                        Turn(card.cardData.id, player.GetPlayerData().id);

                        //TODO: videti da li radi kako treba
                        playerHand.Remove(card);

                        GameController.instance.GetPlayer().DealCard(playerHand);

                        break;
                    }
                }
            }  
        }

        chatText.text = GameController.instance.chatHistory;
    }

    public void Turn(int cardID, int attackedUser)
    {
        GameData gd = GameController.instance.GetGameData();
        List<Player> players = GameController.instance.GetGamePlayers();
        int nextTurn = (GameController.instance.GetPlayer().GetPlayerStateData().turnOrder + 1) % gd.numOfPlayers;
        gd.whoseTurnID = players.Find(p => p.GetPlayerStateData().turnOrder == nextTurn).GetPlayerData().id;

        StartCoroutine(GameController.instance.apiHelper.Turn(gd.id, GameController.instance.GetPlayerData().id, attackedUser, gd.whoseTurnID, cardID));
    }

    public void SkipTurn()
    {
        GameData gd = GameController.instance.GetGameData();
        List<Player> players = GameController.instance.GetGamePlayers();
        int nextTurn = (GameController.instance.GetPlayer().GetPlayerStateData().turnOrder + 1) % gd.numOfPlayers;
        gd.whoseTurnID = players.Find(p => p.GetPlayerStateData().turnOrder == nextTurn).GetPlayerData().id;
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
