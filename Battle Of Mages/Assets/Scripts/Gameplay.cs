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
        if (GameController.instance.CheckTurn())
            turnText.text = "Your Turn!";
        else
            turnText.text = "Player Two turn!";
    }

    private void SetTexts()
    {
        UserData ud = GameController.instance.GetPlayerData();
        PlayerStateData psd = GameController.instance.GetPlayerStateData();

        playerInfoText.text = ud.username.Replace("\"", "") + "#" + ud.tag.Replace("\"", "") +
                              "\nHealth Points: " + psd.healthPoints + "\nMana Points: " + psd.manaPoints;

        //TODO: ovo preko player list
        playerTwoInfoText.text = "Username#tag\nHealth Points: " + 10 + "\nMana Points: " + 5;
    }

    void Start()
    {
        SetTexts();
        ChangeTurnText();
    }

    public void SkipTurn()
    {
        GameData gd = GameController.instance.GetGameData();
        gd.whoseTurnID = 12; //TODO: da ide preko player liste
        PlayerStateData psd = GameController.instance.GetPlayerStateData();
        psd.manaPoints += 1;

        //TODO: odigrati potez kao skip turn da bi se BP updateovala

        GameController.instance.UpdateGameData(gd);
        GameController.instance.UpdatePlayerStateData(psd);

        SetTexts();
        ChangeTurnText();
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
