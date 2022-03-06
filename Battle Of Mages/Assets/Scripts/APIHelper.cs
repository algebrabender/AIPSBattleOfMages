using System.Collections.Generic;
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

	public GameData CreateGame(GameData gd, string terrainType, int userID, string mageType, int numOfSpellCards, int numbOfAttackCards, int numOfBuffCards)
	{
		string link = "https://localhost:5001/Game/CreateGame/" + terrainType + "/" + userID + "/" + mageType
			+ "/" + numOfSpellCards + "/" + numbOfAttackCards + "/" + numOfBuffCards;
		RestClient.Post<GameData>(link, gd).Then(res =>
		{
			//this.LogMessage(JsonUtility.ToJson(res, true));
			this.gd = res;
		}).Catch(err => this.LogMessage(err.Message));

		return this.gd;
	}

	public PlayerStateData GetPlayerStateData(int gameID)
	{
		RestClient.Get<PlayerStateData>("https://localhost:5001/PlayerState/GetPlayerStateForGame/" + gameID).Then(res =>
		{
			//this.LogMessage(JsonUtility.ToJson(res, true));
			this.psd = res;
		}).Catch(err => this.LogMessage(err.Message));

		return this.psd;
	}
}