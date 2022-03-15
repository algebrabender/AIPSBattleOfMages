using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private UserData userData;
    private List<CardData> currentDeck;
    private PlayerStateData playerStateData;
    private List<Card> currentHand = new List<Card>(5);

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

    internal void DealHand()
    {
        for (int i = 0; i < 5; i++)
        {
            Card card = new Card();
            card.cardData = currentDeck[0];

            currentHand.Add(card);
            currentDeck.Remove(currentDeck[0]);

            if (currentDeck[0].fire == 1)
            {
                card.type = "fire";
                continue;
            }
            if (currentDeck[0].ice == 1)
            {
                card.type = "ice";
                continue;
            }
            if (currentDeck[0].earth == 1)
            {
                card.type = "earth";
                continue;
            }
            if (currentDeck[0].air == 1)
            {
                card.type = "air";
                continue;
            }
        }
    }

    internal void DealCard()
    {
        Card card = new Card();
        card.cardData = currentDeck[0];

        if (currentDeck[0].fire == 1)
            card.type = "fire";
        if (currentDeck[0].ice == 1)
            card.type = "ice";
        if (currentDeck[0].earth == 1)
            card.type = "earth";
        if (currentDeck[0].air == 1)
            card.type = "air";

        currentHand.Add(card);
        currentDeck.Remove(currentDeck[0]);
    }

    internal void RemoveCard(Card card)
    {
        currentHand.Remove(card);
    }
}
