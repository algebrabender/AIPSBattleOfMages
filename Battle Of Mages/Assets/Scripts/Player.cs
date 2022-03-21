﻿using System.Collections.Generic;

public class Player
{
    private UserData userData;
    private List<CardData> currentDeck;
    private PlayerStateData playerStateData;
    private string mageType;

    internal void SetPlayer(UserData ud, PlayerStateData psd = null)
    {
        this.userData = ud;
        this.playerStateData = psd;
    }

    internal UserData GetPlayerData()
    {
        return this.userData;
    }

    internal PlayerStateData GetPlayerStateData()
    {
        return this.playerStateData;
    }

    internal void SetMageType(string mage)
    {
        this.mageType = mage;
    }

    internal string GetMageType()
    {
        return this.mageType;
    }

    internal void UpdatePlayerStateData(PlayerStateData psd)
    {
        this.playerStateData = psd;
    }

    internal void UpdateUserData(UserData us)
    {
        this.userData = us;
    }

    internal void SetDeck(List<CardData> d)
    {
        this.currentDeck = d;
    }

    internal List<CardData> GetDeck()
    {
        return this.currentDeck;
    }

    internal void DealHand(List<Card> hand)
    {
        for (int i = 0; i < 5; i++)
        {
            hand[i].cardData = currentDeck[0];
            hand[i].indexInHand = i;

            if (currentDeck[0].fire == 1)
                hand[i].type = "fire";
            if (currentDeck[0].ice == 1)
                hand[i].type = "ice";
            if (currentDeck[0].earth == 1)
                hand[i].type = "earth";
            if (currentDeck[0].air == 1)
                hand[i].type = "air";


            hand[i].SetCard();

            currentDeck.Remove(currentDeck[0]);
        }
    }

    internal void DealCard(List<Card> hand)
    {
        hand[4].cardData = currentDeck[0];
        hand[4].indexInHand = 4;

        if (currentDeck[0].fire == 1)
            hand[4].type = "fire";
        if (currentDeck[0].ice == 1)
            hand[4].type = "ice";
        if (currentDeck[0].earth == 1)
            hand[4].type = "earth";
        if (currentDeck[0].air == 1)
            hand[4].type = "air";

        currentDeck.Remove(currentDeck[0]);
    }

}
