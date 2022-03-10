using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Proyecto26;
using UnityEditor;

public class APIHelper
{
	public UserData ud = null;
	public GameData gd = null;
	public PlayerStateData psd = null;

	private void LogMessage(string message)
	{
#if UNITY_EDITOR
		EditorUtility.DisplayDialog("Title", message, "Ok");
#else
		Debug.Log(message);
#endif
	}

	public void SignUp(string username, string password, string firstName, string lastName)
	{
		RestClient.Post<UserData>("https://localhost:5001/User/CreateUser", new UserData
		{
			  username = username,
			  password = password,
			  firstName = firstName,
			  lastName = lastName
		}).Then(res => 
		{ 
			//this.LogMessage(JsonUtility.ToJson(res, true)); 
			ud = res; 
		}).Catch(err => this.LogMessage(err.Message));
	}

	public void LogIn(string usernameWithTag, string password)
	{
		RestClient.Post<UserData>("https://localhost:5001/User/UserValidating", new UserData
		{
			username = usernameWithTag,
			password = password
		}).Then(res =>
		{
			//this.LogMessage(JsonUtility.ToJson(res, true));
			ud = res;
		}).Catch(err => this.LogMessage(err.Message));
	}

	public void CreateGame(GameData gd, string terrainType, int userID, string mageType, int numOfSpellCards, int numbOfAttackCards, int numOfBuffCards)
	{
		if (this.gd != null)
			return;

		string link = "https://localhost:5001/Game/CreateGame/" + terrainType + "/" + userID + "/" + mageType
			+ "/" + numOfSpellCards + "/" + numbOfAttackCards + "/" + numOfBuffCards;
		RestClient.Post<GameData>(link, gd).Then(res =>
		{
			//this.LogMessage(JsonUtility.ToJson(res, true));
			this.gd = res;
		}).Catch(err => this.LogMessage(err.Message));
	}

	public void AddUserToGame(int gameID, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
	{
		if (this.gd != null)
			return;

		string link = "https://localhost:5001/Game/AddUserToGame/" + gameID + "/" + userID + "/" + mageType 
			+ "/" + numOfSpellCards + "/" + numOfAttackCards + "/" + numOfBuffCards;

		RestClient.Put<GameData>(link, "").Then(res =>
		{
			this.gd = res;
		}).Catch(err => this.LogMessage(err.Message));
	}

	public void GetPlayerStateData(int gameID, int userID)
	{
		if (this.psd != null)
			return;

		RestClient.Get<PlayerStateData>("https://localhost:5001/PlayerState/GetPlayerStateForGame/" + gameID + "/" + userID).Then(res =>
		{
			//this.LogMessage(JsonUtility.ToJson(res, true));
			this.psd = res;
		}).Catch(err => this.LogMessage(err.Message));
	}



	public void GetDeckWithCards(int deckID)
	{
		RestClient.GetArray<CardData>("https://localhost:5001/Card/GetCardsByDeckID/" + deckID).Then(res =>
		{		
			//this.LogMessage(JsonUtility.ToJson(res, true));
			GameController.instance.GetPlayer().SetDeck(res);
		}).Catch(err => this.LogMessage(err.Message));
	}

	public void GetPlayersInGame(int gameID)
	{
		RestClient.GetArray<PlayerStateData>("https://localhost:5001/PlayerState/GetPlayersInGame/" + gameID).Then(res =>
		{
			List<Player> players = new List<Player>(res.Length);
			foreach (var psd in res)
			{
				Player p = new Player();
				p.UpdatePlayerStateData(psd);
				p.UpdateUserData(psd.user);
			}

			GameController.instance.SetGamePlayers(players);

		}).Catch(err => this.LogMessage(err.Message));
	}

	public void Turn(int gameID, int userID, int manaSpent, int attackedUser, int damageDone, int nextUserID, int cardID)
	{
		string link = "https://localhost:5001/Game/Turn/" + gameID + "/" + userID + "/" + manaSpent
					  + "/" + attackedUser + "/" + damageDone + "/" + nextUserID + "/" + cardID;
		RestClient.Put<GameData>(link, null).Then(res =>
		{
			GameController.instance.UpdateGameData(res);
		}).Catch(err => this.LogMessage(err.Message));
	}
}