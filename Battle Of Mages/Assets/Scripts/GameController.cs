using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static public GameController instance;
    private Player player = new Player();
    public WelcomeMenu welcomeMenu;

    public void Awake()
    {
        instance = this;
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
}
