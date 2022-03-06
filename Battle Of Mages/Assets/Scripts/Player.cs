using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private UserData playerData;
    internal bool turn;

    internal void SetPlayer(UserData ud)
    {
        this.playerData = ud;
    }

    internal UserData GetPlayer()
    {
        return this.playerData;
    }
}
