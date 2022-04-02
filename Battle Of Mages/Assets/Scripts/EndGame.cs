using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text endGameText;
    private void Awake()
    {
        endGameText.text = GameController.instance.endGameText;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
