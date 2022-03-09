using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
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

        GameController.instance.apiHelper.CreateGame(gd, terrain, userID, mage, spellCards, attackCards, buffCards);
        GameData newGame = GameController.instance.apiHelper.gd;
        if (newGame != null)
        {
            List<Player> players = new List<Player>();
            players.Add(GameController.instance.GetPlayer());
            PlayerStateData psd;
            GameController.instance.apiHelper.GetPlayerStateData(newGame.id, userID);

            await GameController.instance.signalRConnector.JoinGame(newGame.id, GameController.instance.GetPlayerData().username.Replace("\"", ""));

            psd = GameController.instance.apiHelper.psd;

            if (psd != null)
            {
                GameController.instance.SetGame(newGame, players);

                GameController.instance.UpdatePlayerStateData(psd);

                GameController.instance.apiHelper.GetDeckWithCards(psd.deckID);

                SceneManager.LoadScene(5); //ubaciti promenu u zavisnosti od broja igraca koja scene se prikazuje
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

        GameController.instance.apiHelper.AddUserToGame(gameID, userID, mageType, spellCards, attackCards, buffCards);
        GameData gd = GameController.instance.apiHelper.gd;
        if(gd != null)
        {
            GameController.instance.apiHelper.GetPlayerStateData(gd.id, userID);

            string username = GameController.instance.GetPlayerData().username.Replace("\"", "");
            await GameController.instance.signalRConnector.JoinGame(gameID, username);


            PlayerStateData psd = GameController.instance.apiHelper.psd;

            if (psd != null)
            {
                //TODO: set game

                GameController.instance.apiHelper.GetDeckWithCards(psd.deckID);

                SceneManager.LoadScene(5); //ubaciti promenu u zavisnosti od broja igraca koja scene se prikazuje
            }

            //SceneManager.LoadScene(5); //ubaciti promenu u zavisnosti od broja igraca koja scene se prikazuje
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
