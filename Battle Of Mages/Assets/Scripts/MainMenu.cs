using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text welcomeText;
    public Text invitesText;

    void Start()
    {
        GameController.instance.signalRConnector.OnInviteReceived += UpdateInvites;

        welcomeText.text = "Welcome " + GameController.instance.GetPlayerData().username.Replace("\"", "") +
                            "#" + GameController.instance.GetPlayerData().tag.Replace("\"", "") + "!";
    }

    private void UpdateInvites(InviteData obj)
    {
        var lastInvites = this.invitesText.text;

        if (string.IsNullOrEmpty(lastInvites) == false)
            lastInvites += "\n";

        lastInvites += $"User {obj.userFrom} invited you to Game with ID: {obj.gameID}";
        this.invitesText.text = lastInvites;
    }

    public void CreateNewGame()
    {
        SceneManager.LoadScene(3);
    }

    public void JoinGame()
    {
        SceneManager.LoadScene(4);
    }

    public void JoinRandomGame()
    {
        SceneManager.LoadScene(8);
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}