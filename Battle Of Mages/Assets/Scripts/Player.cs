using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private UserData playerData;
    private DeckData deckData;

    internal void SetPlayer(UserData ud)
    {
        this.playerData = ud;
    }

    internal UserData GetPlayerData()
    {
        return this.playerData;
    }

    internal void SetDeck(DeckData dd)
    {
        this.deckData = dd;
    }

    internal DeckData GetDeckData()
    {
        return this.deckData;
    }
}
