using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
       //TODO: postaviti tekst ko je winner
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(2);
    }
}
