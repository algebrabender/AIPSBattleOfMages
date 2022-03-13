using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class WelcomeMenu : MonoBehaviour
{
    public InputField userNameEntryField;
    public InputField passwordEntryField;
    public InputField firstNameEntryField;
    public InputField lastNameEntryField;

    public string userNameText;
    public string passwordText;
    public string firstNameText;
    public string lastNameText;

    private bool ProcessInput(string username, string password, int sceneNumber, string firstName = "", string lastName = "")
    {
        if (sceneNumber == 1)
        {
            StartCoroutine(GameController.instance.apiHelper.SignUp(username, password, firstName, lastName));
        }
        else
        {
            StartCoroutine(GameController.instance.apiHelper.LogIn(username, password));
        }

        if (GameController.instance.apiHelper.ud != null)
        {
            GameController.instance.SetPlayerData(GameController.instance.apiHelper.ud);
            GameController.instance.JoinApp();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LogIn()
    {
        //await signalRConnector.JoinApp(userID);

        userNameText = userNameEntryField.text;
        passwordText = passwordEntryField.text;

        if (SuccesfullLogIn())
        {
            SceneManager.LoadScene(2);

            userNameEntryField.text = string.Empty;
            passwordEntryField.text = string.Empty;
        }
        //else deo ako je doslo do greske prilikom api poziva
    }

    private bool SuccesfullLogIn()
    {
        return this.ProcessInput(userNameText, passwordText, 2);
    }

    public void NewSignUp()
    {
        SceneManager.LoadScene(1);
    }

    public void SignUp()
    {
        userNameText = userNameEntryField.text;
        passwordText = passwordEntryField.text;
        firstNameText = firstNameEntryField.text;
        lastNameText = lastNameEntryField.text;

        if (SuccesfulSignUp())
        {
            SceneManager.LoadScene(2);

            userNameEntryField.text = string.Empty;
            passwordEntryField.text = string.Empty;
            firstNameEntryField.text = string.Empty;
            lastNameEntryField.text = string.Empty;
        }
        //else deo ako je doslo do greske prilikom api poziva
    }

    private bool SuccesfulSignUp()
    {
        return this.ProcessInput(userNameText, passwordText, 1, firstNameText, lastNameText);
    }

    public void Quit()
    {
        GameController.instance.Quit();
    }
}
