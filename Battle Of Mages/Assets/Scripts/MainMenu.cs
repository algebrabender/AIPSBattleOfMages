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
        welcomeText.text = "Welcome " + GameController.instance.GetPlayer().username + "#" + GameController.instance.GetPlayer().tag + "!";
    }

    public void NewGame()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        //for editor
        EditorApplication.isPlaying = false;
#else
            //for build
            Application.Quit();
#endif
    }
}