using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private UserData playerData;

    public void SetPlayer(UserData ud)
    {
        this.playerData = ud;
    }

    public UserData GetPlayer()
    {
        return this.playerData;
    }
}
