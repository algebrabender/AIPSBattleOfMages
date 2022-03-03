using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "CardGame/Card")]
public class CardData : MonoBehaviour
{
    public enum CardType
    {
        AttackSpell,
        HealSpell,
        ReduceCost,
        AddDamage
    }

    public string cardTitle;
    public string cardDescription;
    public CardType cardType;
    public int manaCost;
    public int damage;
    public int numberInDeck;

    //non database data - TODO: CHECK IF CAN BE MOVED
    public Sprite cardImage;
    public Sprite frameImage;
}
