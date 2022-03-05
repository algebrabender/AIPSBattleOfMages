using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetUp : MonoBehaviour
{
    public InputField gameIDEntryField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void JoinGame()
    {
        int gameID;
        Int32.TryParse(gameIDEntryField.text, out gameID);

        string username = GameController.instance.GetPlayer().username.Replace("\"", "");

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
