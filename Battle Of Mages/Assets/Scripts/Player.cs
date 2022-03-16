using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Models;

public class Player
{
    private UserData userData;
    private List<CardData> currentDeck;
    private PlayerStateData playerStateData;
    private List<Card> currentHand = new List<Card>(5);
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

    internal List<Card> GetHand()
    {
        return this.currentHand;
    }

    internal void DealHand(List<Card> hand)
    {
        string terrainType = GameController.instance.GetGameTerrain();

        for (int i = 0; i < 5; i++)
        {
            Card card = new Card();
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

            switch (hand[i].cardData.type)
            {
                case "attack":
                    {
                        if (terrainType == hand[i].type)
                        {
                            if (terrainType == mageType)
                                hand[i].SetStrategy(new DoubleBoostedAttack());
                            else
                                hand[i].SetStrategy(new BoostedAttack());
                        }
                        else if (mageType == card.type)
                            hand[i].SetStrategy(new BoostedAttack());
                        else
                            hand[i].SetStrategy(new BaseAttack());
                    }
                    break;
                case "heal":
                    {
                        if (terrainType == card.type)
                        {
                            if (terrainType == mageType)
                                hand[i].SetStrategy(new DoubleBoostedHeal());
                            else
                                hand[i].SetStrategy(new BoostedHeal());
                        }
                        else if (mageType == card.type)
                            hand[i].SetStrategy(new BoostedHeal());
                        else
                            hand[i].SetStrategy(new BaseHeal());
                    }
                    break;
                case "add damage":
                    hand[i].SetStrategy(new AddDamage());
                    break;
                case "reduce cost":
                    hand[i].SetStrategy(new ReduceCost());
                    break;
            }

            hand[i].SetCard();

            currentHand.Add(card);
            currentDeck.Remove(currentDeck[0]);
        }
    }

    internal void DealCard()
    {
        string terrainType = GameController.instance.GetGameTerrain();

        Card card = new Card();
        card.cardData = currentDeck[0];
        card.indexInHand = 4;

        if (currentDeck[0].fire == 1)
            card.type = "fire";
        if (currentDeck[0].ice == 1)
            card.type = "ice";
        if (currentDeck[0].earth == 1)
            card.type = "earth";
        if (currentDeck[0].air == 1)
            card.type = "air";

        switch (card.cardData.type)
        {
            case "attack":
                {
                    if (terrainType == card.type)
                    {
                        if (terrainType == mageType)
                            card.SetStrategy(new DoubleBoostedAttack());
                        else
                            card.SetStrategy(new BoostedAttack());
                    }
                    else if (mageType == card.type)
                        card.SetStrategy(new BoostedAttack());
                    else
                        card.SetStrategy(new BaseAttack());
                }
                break;
            case "heal":
                {
                    if (terrainType == card.type)
                    {
                        if (terrainType == mageType)
                            card.SetStrategy(new DoubleBoostedHeal());
                        else
                            card.SetStrategy(new BoostedHeal());
                    }
                    else if (mageType == card.type)
                        card.SetStrategy(new BoostedHeal());
                    else
                        card.SetStrategy(new BaseHeal());
                }
                break;
            case "add damage":
                card.SetStrategy(new AddDamage());
                break;
            case "reduce cost":
                card.SetStrategy(new ReduceCost());
                break;
        }

        currentHand.Add(card);
        currentDeck.Remove(currentDeck[0]);
    }

    internal void RemoveCard(int index)
    {
        currentHand.RemoveAt(index);
    }
}
