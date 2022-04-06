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
    public Image icon = null;
    public Sprite fireCard = null;
    public Sprite iceCard = null;
    public Sprite earthCard = null;
    public Sprite airCard = null;
    public Sprite frame = null;
    public Sprite fireIcon = null;
    public Sprite iceIcon = null;
    public Sprite airIcon = null;
    public Sprite earthIcon = null;
    internal bool clicked = false;

    public void SetCard()
    {
        titleText.text = cardData.title;
        descriptionText.text = cardData.description;
        manaCostText.text = cardData.manaCost.ToString();
        damageText.text = cardData.damage.ToString();
        icon.gameObject.SetActive(true);

        switch (type)
        {
            
            case "fire":
                cardImage.sprite = frame;
                cardImage.color = new Color(1.0f, 0.596f, 0.24f);
                icon.sprite = fireIcon;
                break;
            case "ice":
                cardImage.sprite = frame;
                cardImage.color = new Color(0.75f, 0.88f, 1.0f);
                icon.sprite = iceIcon;
                break;
            case "earth":
                cardImage.sprite = frame;
                cardImage.color = new Color(0.68f, 0.43f, 0.31f);
                icon.sprite = earthIcon;
                break;
            case "air":
                cardImage.sprite = frame;
                cardImage.color = new Color(0.87f, 0.99f, 1.0f);
                icon.sprite = airIcon;
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
