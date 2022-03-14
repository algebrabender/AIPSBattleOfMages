using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public SignalRConnector signalRConnector;
    public APIHelper apiHelper = new APIHelper();
    private Player player = new Player();
    private Game game = new Game();
    public string chatHistory = "";

    public WelcomeMenu welcomeMenu;

    public async Task Awake()
    {
        instance = this;

        signalRConnector = new SignalRConnector();

        await signalRConnector.InitAsync();

        DontDestroyOnLoad(this.gameObject);
    }

    public Player GetPlayer()
    {
        return this.player;
    }

    public void SetPlayerData(UserData ud, PlayerStateData psd = null)
    {
        this.player.SetPlayer(ud, psd);
    }

    public UserData GetPlayerData()
    {
        return this.player.GetPlayerData();
    }

    public void SetGame(GameData gd, List<Player> players)
    {
        this.game.SetGame(gd, players);
    }

    public void SetGamePlayers(List<Player> players)
    {
        this.game.SetGamePlayers(players);
    }

    public void SetGameData(GameData gd)
    {
        this.game.SetGameData(gd);
    }

    public GameData GetGameData()
    {
        return this.game.GetGameData();
    }

    public void UpdateGameData(GameData gd)
    {
        this.game.UpdateGameData(gd);
    }

    public void UpdateGamePlayers(Player player)
    {
        this.game.AddPlayer(player);
    }

    public PlayerStateData GetPlayerStateData()
    {
        return this.player.GetPlayerStateData();
    }

    public void UpdatePlayerStateData(PlayerStateData psd)
    {
        this.player.UpdatePlayerStateData(psd);
    }

    public List<Player> GetGamePlayers()
    {
        return this.game.GetGamePlayers();
    }

    public bool CheckTurn()
    {
        if (game.GetGameData().whoseTurnID == player.GetPlayerData().id)
            return true;
        else
            return false;
    }

    //TODO: proveriti
    public async void JoinApp()
    {
        await signalRConnector.JoinApp(player.GetPlayerData().id);
    }

    //TODO: proveriti
    public async void LeaveApp()
    {
        await signalRConnector.LeaveApp(player.GetPlayerData().id);
    }

    public void Quit()
    {
        LeaveApp();
#if UNITY_EDITOR
        //for editor
        EditorApplication.isPlaying = false;
#else
            //for build
            Application.Quit();
#endif
    }
}
