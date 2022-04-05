using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
    internal bool clicked = false;

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

    public void UpdateCard(bool mana, int amount)
    {
        if (mana)
            manaCostText.text = (cardData.manaCost - amount).ToString();
        else
            damageText.text = (cardData.damage + amount).ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cardData.title != "")
            highlightImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cardData.title != "")
            highlightImage.gameObject.SetActive(false);
    }
}
