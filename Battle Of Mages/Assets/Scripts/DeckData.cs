using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<CardData> cardDatas;
    int numberOfCard;

    public void Create()
    {
        List<CardData> cardDataInOrder = new List<CardData>(numberOfCard);
    }
}
