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

    void Start()
    {
        welcomeText.text = "Welcome " + GameController.instance.GetPlayer().username.Replace("\"", "") + 
                            "#" + GameController.instance.GetPlayer().tag.Replace("\"", "") + "!";
    }

    public void CreateNewGame()
    {
        SceneManager.LoadScene(3);
    }

    public void JoinGame()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}