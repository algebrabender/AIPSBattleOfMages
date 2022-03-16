using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour, ICardContext
{
    public CardData cardData = null;
    public string type = null;
    public int indexInHand = -1;
    public Text titleText = null;
    public Text descriptionText = null;
    public Text damageText = null;
    public Text manaCostText = null;
    public Image cardImage = null;
    public Image highlightImage = null;
    public Sprite fireCard = null;
    public Sprite iceCard = null;
    public Sprite earthCard = null;
    public Sprite airCard = null;

    private ICardStrategy cardStrategy;

    public void SetCard()
    {
        titleText.text = cardData.title;
        descriptionText.text = cardData.description;
        manaCostText.text = cardData.manaCost.ToString();
        damageText.text = cardData.damage.ToString();

        switch(type)
        {
            case "fire":
                cardImage.sprite = fireCard;
                break;
            case "ice":
                cardImage.sprite = iceCard;
                break;
            case "earth":
                cardImage.sprite = earthCard;
                break;
            case "air":
                cardImage.sprite = airCard;
                break;
        }
    }

    public void SetStrategy(ICardStrategy cardStrategy)
    {
        this.cardStrategy = cardStrategy;
    }

    public void Turn(int gameID, int turnUserID, int attackedUserID, int nextUserID, CardData cardData)
    {
        this.cardStrategy.Turn(gameID, turnUserID, attackedUserID, nextUserID, cardData);
    }
}
