using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Card", menuName = "CardGame/Card")]
public class CardData
{
    public enum CardType
    {
        AttackSpell,
        HealSpell,
        ReduceCost,
        AddDamage
    }
    public int id;
    public string title;
    public string description;
    public CardType cardType; //non database data
    public string type;
    public int manaCost;
    public int damage;
    public int numberInDeck;
    public int fire;
    public int ice;
    public int air;
    public int earth;

    //non database data - TODO: CHECK IF CAN BE MOVED
    public Sprite cardImage;
    public Sprite frameImage;
}
