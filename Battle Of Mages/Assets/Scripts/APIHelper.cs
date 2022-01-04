using System.Net;
using System.IO;
using UnityEngine;

public static class APIHelper
{
    public static CardData GetNewCardData() //DEAL CARD
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.chucknorris.io/jokes/random");

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());

        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<CardData>(json);
    }
}