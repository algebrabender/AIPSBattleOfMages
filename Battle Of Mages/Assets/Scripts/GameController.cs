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
    public WelcomeMenu welcomeMenu;

    public async Task Awake()
    {
        instance = this;

        signalRConnector = new SignalRConnector();

        await signalRConnector.InitAsync();

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetPlayer(UserData ud)
    {
        this.player.SetPlayer(ud);
    }

    public UserData GetPlayer()
    {
        return this.player.GetPlayer();
    }

    public async void JoinApp()
    {
        await signalRConnector.JoinApp(player.GetPlayer().id);
    }

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
