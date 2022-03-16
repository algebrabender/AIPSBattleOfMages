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
    public Text titleText = null;
    public Text descriptionText = null;
    public Text damageText = null;
    public Text manaCostText = null;
    public Image cardImage = null;
    public Image frameImage = null;
    public Image burnImage = null;
    public Image destroyImage = null;

    private ICardStrategy cardStrategy;

    public void Initialize()
    {
        if (cardData == null)
        {
            Debug.LogError("Card has no CardData");
            return;
        }

        titleText.text = cardData.title;
        descriptionText.text = cardData.description;
        manaCostText.text = cardData.manaCost.ToString();
        damageText.text = cardData.damage.ToString();

        //cardImage.sprite = cardData.cardImage;
        //frameImage.sprite = cardData.frameImage;

        //costImage.sprite = GameController.instance.healthNumbers[cardData.cost];
        //damageImage.sprite = GameController.instance.damageNumbers[cardData.damage];
    }

    public void SetStrategy(ICardStrategy cardStrategy)
    {
        this.cardStrategy = cardStrategy;
    }

    public async Task<GameData> Turn()
    {
        return await this.cardStrategy.Turn();
    }
}
