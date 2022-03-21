using System.Collections.Generic;

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

        currentHand.Add(card);
        currentDeck.Remove(currentDeck[0]);
    }

    internal void RemoveCard(int index)
    {
        currentHand.RemoveAt(index);
    }
}
