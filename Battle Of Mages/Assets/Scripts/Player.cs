using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UserData userData;
    private List<CardData> currentDeck;
    private PlayerStateData playerStateData;
    private string mageType;
    internal bool clicked = false;
    public Image highlightImage = null;

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

    internal void DealCard(List<Card> hand, Card playedCard)
    {
        int index = hand.FindIndex(c => c.cardData.id == playedCard.cardData.id);
        hand[index].cardData = currentDeck[0];

        if (currentDeck[0].fire == 1)
            hand[index].type = "fire";
        if (currentDeck[0].ice == 1)
            hand[index].type = "ice";
        if (currentDeck[0].earth == 1)
            hand[index].type = "earth";
        if (currentDeck[0].air == 1)
            hand[index].type = "air";

        hand[index].SetCard();

        currentDeck.Remove(currentDeck[0]);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameController.instance.GetGamePlayers().First(p => p.userData == this.userData).clicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlightImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlightImage.gameObject.SetActive(false);
    }
}
