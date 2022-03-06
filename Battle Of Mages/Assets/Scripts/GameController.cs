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

    public void SetPlayerData(UserData ud)
    {
        this.player.SetPlayer(ud);
    }

    public UserData GetPlayerData()
    {
        return this.player.GetPlayer();
    }

    public void SetGame(GameData gd, List<Player> players, PlayerStateData psd)
    {
        this.game.SetGame(gd, players, psd);
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

    public List<Player> GetGamePlayers()
    {
        return this.game.GetGamePlayers();
    }

    //TODO: proveriti
    public async void JoinApp()
    {
        await signalRConnector.JoinApp(player.GetPlayer().id);
    }

    //TODO: proveriti
    public async void LeaveApp()
    {
        await signalRConnector.LeaveApp(player.GetPlayer().id);
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
