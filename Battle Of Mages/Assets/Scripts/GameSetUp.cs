using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetUp : MonoBehaviour
{
    public InputField gameIDEntryField;
    public Dropdown numOfPlayersDropdown;
    public Dropdown typeOfMageDropdown;
    public Dropdown typeOfTerrainDropdown;
    public InputField numOfSpellCards;
    public InputField numOfAttackCards;
    public InputField numOfBuffCards;

    public enum MagicType
    {
        Air = 0,
        Earth,
        Fire,
        Ice
    }

    private bool GetData(int gameID, int userID)
    {
        StartCoroutine(GameController.instance.apiHelper.GetPlayerStateData(gameID, userID));
        PlayerStateData psd = GameController.instance.apiHelper.psd;

        if (psd != null)
        {
            GameController.instance.UpdatePlayerStateData(psd);

            StartCoroutine(GameController.instance.apiHelper.GetDeckWithCards(psd.deckID, userID));

            return true;
        }

        return false;
    }

    public async void CreateGame()
    {
        string terrain = ((MagicType)typeOfTerrainDropdown.value).ToString();
        string mage = ((MagicType)typeOfMageDropdown.value).ToString();
        int userID = GameController.instance.GetPlayerData().id;
        int spellCards = Int32.Parse(numOfSpellCards.text);
        int attackCards = Int32.Parse(numOfAttackCards.text);
        int buffCards = Int32.Parse(numOfBuffCards.text);

        GameData gd = new GameData();
        gd.createdGameUserID = userID;
        gd.whoseTurnID = userID;
        gd.numOfPlayers = numOfPlayersDropdown.value + 2;

        StartCoroutine(GameController.instance.apiHelper.CreateGame(gd, terrain, userID, mage, spellCards, attackCards, buffCards));

        GameData newGame = GameController.instance.apiHelper.gd;

        if (newGame != null)
        {
            List<Player> players = new List<Player>();
            players.Add(GameController.instance.GetPlayer());

            await GameController.instance.signalRConnector.JoinGame(newGame.id, GameController.instance.GetPlayerData().username.Replace("\"", ""));

            StartCoroutine(GameController.instance.apiHelper.GetMageType(newGame.id));

            if (GetData(newGame.id, userID))
            {
                GameController.instance.SetGame(newGame, players);

                StartCoroutine(GameController.instance.apiHelper.GetTerrainType(newGame.id));

                if (gd.numOfPlayers == 2)
                    SceneManager.LoadScene(5);
                else if (gd.numOfPlayers == 3)
                    SceneManager.LoadScene(6);
                else
                    SceneManager.LoadScene(7);
            }
        }
    }

    public async void JoinGame()
    {
        string mageType = ((MagicType)typeOfMageDropdown.value).ToString();
        int userID = GameController.instance.GetPlayerData().id;
        int spellCards = Int32.Parse(numOfSpellCards.text);
        int attackCards = Int32.Parse(numOfAttackCards.text);
        int buffCards = Int32.Parse(numOfBuffCards.text);
        int gameID = Int32.Parse(gameIDEntryField.text);

        StartCoroutine(GameController.instance.apiHelper.AddUserToGame(gameID, userID, mageType, spellCards, attackCards, buffCards));

        GameData gd = GameController.instance.apiHelper.gd;

        if(gd != null)
        {
            string username = GameController.instance.GetPlayerData().username.Replace("\"", "");
            await GameController.instance.signalRConnector.JoinGame(gameID, username);

            StartCoroutine(GameController.instance.apiHelper.GetMageType(gd.id));

            if (GetData(gd.id, userID))
            {
                GameController.instance.SetGameData(gd);

                StartCoroutine(GameController.instance.apiHelper.GetTerrainType(gd.id));

                if (gd.numOfPlayers == 2)
                    SceneManager.LoadScene(5);
                else if (gd.numOfPlayers == 3)
                    SceneManager.LoadScene(6);
                else
                    SceneManager.LoadScene(7);
            }
        }

    }

    public async void JoinRandomGame()
    {
        string mageType = ((MagicType)typeOfMageDropdown.value).ToString();
        int userID = GameController.instance.GetPlayerData().id;
        int spellCards = Int32.Parse(numOfSpellCards.text);
        int attackCards = Int32.Parse(numOfAttackCards.text);
        int buffCards = Int32.Parse(numOfBuffCards.text);

        StartCoroutine(GameController.instance.apiHelper.JoinRandomGame(userID, mageType, spellCards, attackCards, buffCards));

        GameData gd = GameController.instance.apiHelper.gd;

        if (gd != null)
        {
            string username = GameController.instance.GetPlayerData().username.Replace("\"", "");
            await GameController.instance.signalRConnector.JoinGame(gd.id, username);

            StartCoroutine(GameController.instance.apiHelper.GetMageType(gd.id));

            if (GetData(gd.id, userID))
            {
                GameController.instance.SetGameData(gd);

                StartCoroutine(GameController.instance.apiHelper.GetTerrainType(gd.id));

                if (gd.numOfPlayers == 2)
                    SceneManager.LoadScene(5);
                else if (gd.numOfPlayers == 3)
                    SceneManager.LoadScene(6);
                else
                    SceneManager.LoadScene(7);
            }
        }
    }

    public void Back()
    {
        SceneManager.LoadScene(2);
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
