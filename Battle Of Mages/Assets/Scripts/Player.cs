using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private UserData playerData;
    private DeckData deckData;
    private PlayerStateData playerStateData;

    internal void SetPlayer(UserData ud, PlayerStateData psd = null)
    {
        this.playerData = ud;
        this.playerStateData = psd;
    }

    internal UserData GetPlayerData()
    {
        return this.playerData;
    }

    internal PlayerStateData GetPlayerStateData()
    {
        return this.playerStateData;
    }

    internal void UpdatePlayerStateData(PlayerStateData psd)
    {
        this.playerStateData = psd;
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
