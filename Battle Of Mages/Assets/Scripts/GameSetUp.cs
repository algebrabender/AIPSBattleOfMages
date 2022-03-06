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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateGame()
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

        GameData newGame = GameController.instance.apiHelper.CreateGame(gd, terrain, userID, mage, spellCards, attackCards, buffCards);

        List<Player> players = new List<Player>();
        players.Add(GameController.instance.GetPlayer());
        PlayerStateData psd = GameController.instance.apiHelper.GetPlayerStateData(newGame.id);
        GameController.instance.SetGame(newGame, players, psd);

        GameController.instance.GetPlayer().turn = true;

        SceneManager.LoadScene(5);
    }

    public async void JoinGame()
    {
        int gameID;
        Int32.TryParse(gameIDEntryField.text, out gameID);

        string username = GameController.instance.GetPlayerData().username.Replace("\"", "");

        await GameController.instance.signalRConnector.JoinGame(gameID, username);

        SceneManager.LoadScene(5);
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
