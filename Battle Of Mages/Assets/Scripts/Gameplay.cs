using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Text playerInfoText;
    public Text playerTwoInfoText;
    public Text turnText;

    private void ChangeTurnText()
    {
        if (GameController.instance.GetPlayer().turn)
            turnText.text = "Your Turn!";
        else
            turnText.text = "Player Two turn!";
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInfoText.text = GameController.instance.GetPlayerData().username.Replace("\"", "") + "#" + GameController.instance.GetPlayerData().tag.Replace("\"", "") +
                              "\nHealth Points: " + 10 + "\nMana Points: " + 5;
        playerTwoInfoText.text = "Username#tag\nHealth Points: " + 10 + "\nMana Points: " + 5;
        
        ChangeTurnText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SkipTurn()
    {
        GameController.instance.GetPlayer().turn = false;
        ChangeTurnText();
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
