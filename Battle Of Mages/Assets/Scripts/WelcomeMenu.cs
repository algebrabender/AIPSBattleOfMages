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
    public static SignalRConnector signalRConnector;
    //private APIHelper apiHelper = new APIHelper(); 
    public InputField userNameEntryField;
    public InputField passwordEntryField;
    public InputField firstNameEntryField;
    public InputField lastNameEntryField;
    public InputField userIDEntryField;

    public async Task Start()
    {
        signalRConnector = new SignalRConnector();

        await signalRConnector.InitAsync();
    }

    public string userNameText;
    public string passwordText;
    public string firstNameText;
    public string lastNameText;

    public void UserNameEntered()
    {
        this.LogCurrentUserName();

        passwordEntryField.ActivateInputField();
    }

    public void PasswordEntered()
    {
        this.LogCurrentPassword();
    }

    public void FirstNameEntered()
    {
        this.LogCurrentFirstName();

        lastNameEntryField.ActivateInputField();
    }

    public void LastNameEntered()
    {
        this.LogCurrentLastName();

        userNameEntryField.ActivateInputField();
    }

    private void LogCurrentUserName()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //TODO: ako je signup prov

            userNameText = userNameEntryField.text;
        }
        else
        {
            string text = userNameEntryField.text;
            int textLen = text.Length;
            if (text.Substring(textLen - 5).Contains("#"))
            {
                string usernameTag = text.Split('#')[1];
                if (usernameTag.Length == 4)
                    userNameText = text;
                else
                    userNameEntryField.text = "#Tag must have 4 numbers!";
            }
            else
                userNameEntryField.text = "Please insert Username with #Tag!";
        }
    }

    private void LogCurrentPassword()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            string text = passwordEntryField.text;

            if (text.Length < 6)
            {
                passwordEntryField.text = "Password must have at least 6 characters!";
                passwordEntryField.contentType = InputField.ContentType.Standard;
            }
            else if (!text.Any(char.IsDigit))
            {
                passwordEntryField.text = "Password must have at least 1 number!";
                passwordEntryField.contentType = InputField.ContentType.Standard;
            }
            else if (!text.Any(char.IsUpper))
            {
                passwordEntryField.text = "Password must have at least 1 uppercase letter!";
                passwordEntryField.contentType = InputField.ContentType.Standard;
            }
            else
            {
                passwordText = passwordEntryField.text;
            }
            //moze i da se doda za special characters preko regexa a i ne mora

            //prebaciti prikaz greske negde drugde zbog podesavanja contenttypea
        }
        else
        {
            passwordText = passwordEntryField.text;
        }
    }

    private void LogCurrentFirstName()
    {
        //TODO: provera neka?
        firstNameText = firstNameEntryField.text;
    }

    private void LogCurrentLastName()
    {
        //TODO: provera neka?
        lastNameText = lastNameEntryField.text;
    }

    private void ProcessInput(string username, string password, int sceneNumber, string firstname = "", string lastname = "")
    {
        //JOS NESTO PORED USERNAME/PASSWORD
        //TODO: if 1 -> api helper sign up else if 2 -> api helper log in
        //da vraca string?
    }

    public async void LogIn()
    {
        int userID;
        Int32.TryParse(userIDEntryField.text, out userID);

        await signalRConnector.JoinApp(userID);

        userNameText = userNameEntryField.text;

        SceneManager.LoadScene(5);

        //if (SuccesfullLogIn())
        //{
        //    SceneManager.LoadScene(2);

        //    userNameEntryField.text = string.Empty;
        //    passwordEntryField.text = string.Empty;
        //}
        //else deo ako je doslo do greske prilikom api poziva
    }

    private bool SuccesfullLogIn()
    {
        this.ProcessInput(userNameText, passwordText, 2);

        return true;
    }

    public void NewSignUp()
    {
        SceneManager.LoadScene(1);
    }

    public void SignUp()
    {
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
        this.ProcessInput(userNameText, passwordText, 1, firstNameText, lastNameText);

        return true;
    }

    public async void Quit()
    {
        int userID;
        Int32.TryParse(userIDEntryField.text, out userID);

        await signalRConnector.LeaveApp(userID);
#if UNITY_EDITOR
        //for editor
        EditorApplication.isPlaying = false;
#else
            //for build
            Application.Quit();
#endif
    }
}
