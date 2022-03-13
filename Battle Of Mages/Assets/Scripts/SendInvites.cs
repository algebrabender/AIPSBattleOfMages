using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SendInvites : MonoBehaviour
{
    public InputField player1tag;
    //za sad jedan po jedan poziv


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Send()
    {
        string usernametag = player1tag.text;
        string username = usernametag.Substring(0, usernametag.Length - 5);
        //int tag = Int32.Parse(usernametag.Substring(usernametag.Length - 5, 4));

        string tag = usernametag.Substring(usernametag.Length - 4, 4);

        int gameID = GameController.instance.apiHelper.gd.id;
        int userFromID = GameController.instance.apiHelper.psd.userID;

        StartCoroutine(GameController.instance.apiHelper.SendInvite(gameID, username, tag, userFromID));

    }

    public void Back()
    {
        SceneManager.LoadScene(GameController.instance.apiHelper.gd.numOfPlayers + 3);
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
