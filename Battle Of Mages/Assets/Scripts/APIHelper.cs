using System.Collections.Generic;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

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

	public IEnumerator SignUp(string username, string password, string firstName, string lastName)
	{
		UserData udString = new UserData
		{
			username = username,
			password = password,
			firstName = firstName,
			lastName = lastName
		};

		using (UnityWebRequest req = UnityWebRequest.Post("https://localhost:5001/User/CreateUser", "POST"))
		{
			req.SetRequestHeader("Content-Type", "application/json");
			req.SetRequestHeader("accept", "*/*");
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(udString));
			req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);

			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			this.ud = JsonConvert.DeserializeObject<UserData>(req.downloadHandler.text);
		}
	}

	public IEnumerator LogIn(string usernameWithTag, string password)
	{
		UserData udString = new UserData {
			username = usernameWithTag,
			password = password
		};

		using (UnityWebRequest req = UnityWebRequest.Post("https://localhost:5001/User/UserValidating", "POST"))
		{
			req.SetRequestHeader("Content-Type", "application/json");
			req.SetRequestHeader("accept", "*/*");
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(udString));
			req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);

			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			this.ud = JsonConvert.DeserializeObject<UserData>(req.downloadHandler.text);
		}
	}

	public IEnumerator CreateGame(GameData gd, string terrainType, int userID, string mageType, int numOfSpellCards, int numbOfAttackCards, int numOfBuffCards)
	{
		if (this.gd != null)
			yield break;

		string link = "https://localhost:5001/Game/CreateGame/" + terrainType + "/" + userID + "/" + mageType
			+ "/" + numOfSpellCards + "/" + numbOfAttackCards + "/" + numOfBuffCards;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SetRequestHeader("Content-Type", "application/json");
			req.SetRequestHeader("accept", "*/*");
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(gd));
			req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);

			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			this.gd = JsonConvert.DeserializeObject<GameData>(req.downloadHandler.text);

			GameController.instance.turnIndex = 0;
		}
	}

	public IEnumerator AddUserToGame(int gameID, int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
	{
		if (this.gd != null)
			yield break;

		string link = "https://localhost:5001/Game/AddUserToGame/" + gameID + "/" + userID + "/" + mageType 
			+ "/" + numOfSpellCards + "/" + numOfAttackCards + "/" + numOfBuffCards;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SetRequestHeader("accept", "*/*");

			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			this.gd = JsonConvert.DeserializeObject<GameData>(req.downloadHandler.text);
		}
	}
	
	public IEnumerator JoinRandomGame(int userID, string mageType, int numOfSpellCards, int numOfAttackCards, int numOfBuffCards)
	{
		if (this.gd != null)
			yield break;

		string link = "https://localhost:5001/Game/JoinRandomGame/" + userID + "/" + mageType
			+ "/" + numOfSpellCards + "/" + numOfAttackCards + "/" + numOfBuffCards;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SetRequestHeader("accept", "*/*");

			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			this.gd = JsonConvert.DeserializeObject<GameData>(req.downloadHandler.text);
		}
	}

	public IEnumerator GetPlayerStateData(int gameID, int userID)
	{
		if (this.psd != null)
			yield break;

		using (UnityWebRequest req = UnityWebRequest.Get("https://localhost:5001/PlayerState/GetPlayerStateForGame/" + gameID + "/" + userID))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			PlayerStateData data = JsonConvert.DeserializeObject<PlayerStateData>(req.downloadHandler.text);

			this.psd = data;
		}
	}

	public IEnumerator GetDeckWithCards(int deckID, int userID)
	{
		using (UnityWebRequest req = UnityWebRequest.Get("https://localhost:5001/Card/GetCardsByDeckID/" + deckID))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}
	
			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			List<CardData> data = JsonConvert.DeserializeObject<List<CardData>>(req.downloadHandler.text);

			if (GameController.instance.GetPlayerData().id == userID)
				GameController.instance.GetPlayer().SetDeck(data);
			else
				GameController.instance.GetGamePlayers().Find(p => p.GetPlayerData().id == userID).SetDeck(data);
		}
	}

	public IEnumerator GetGamePlayers(int gameID)
	{
		using (UnityWebRequest req = UnityWebRequest.Get("https://localhost:5001/PlayerState/GetPlayersInGame/" + gameID))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			List<PlayerStateData> data = JsonConvert.DeserializeObject<List<PlayerStateData>>(req.downloadHandler.text);
			//Debug.Log(data.Count);
			UserData currentPlayerData = GameController.instance.GetPlayerData();
			List<Player> players = new List<Player>(data.Count - 1);
			foreach (var psd in data)
			{
				if (psd.user.id == currentPlayerData.id)
					continue;

				Player p = new Player();
				p.UpdatePlayerStateData(psd);
				p.UpdateUserData(psd.user);
				players.Add(p);
			}

			GameController.instance.SetGamePlayers(players);
		}
	}

	public IEnumerator Turn(int gameID, int userID, int attackedUser, int nextUserID, int cardID)
	{
		string link = "https://localhost:5001/Game/Turn/" + gameID + "/" + userID
					  + "/" + attackedUser + "/" + nextUserID + "/" + cardID;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			GameController.instance.UpdateGameData(JsonConvert.DeserializeObject<GameData>(req.downloadHandler.text));
		}
	}

	public IEnumerator SkipTurn(int gameID, int userID, int nextUserID)
	{
		string link = "https://localhost:5001/Game/SkipTurn/" + gameID + "/" + userID + "/" + nextUserID;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			GameController.instance.UpdateGameData(JsonConvert.DeserializeObject<GameData>(req.downloadHandler.text));
		}

	}

	public IEnumerator SendInvite(int gameID, string username, string tag, int userFromID)
	{
		string link = "https://localhost:5001/Game/SendInvite/" + gameID + "/" + username + "/" + tag + "/" + userFromID;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			LogMessage(req.downloadHandler.text);
		}
	}

	public IEnumerator GetTerrainType(int gameID)
	{
		using (UnityWebRequest req = UnityWebRequest.Get("https://localhost:5001/Game/GetGameTerrainType/" + gameID))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			GameController.instance.SetGameTerrain(req.downloadHandler.text.Replace("\"", ""));
		}
	}

	public IEnumerator GetMageType(int gameID)
	{
		using (UnityWebRequest req = UnityWebRequest.Get("https://localhost:5001/User/GetUserMageType/" + GameController.instance.GetPlayerData().id + "/" + gameID))
		{
			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}

			GameController.instance.SetPlayerMageType(req.downloadHandler.text.Replace("\"", ""));
		}
	}

	public IEnumerator RemoveUserFromGame(int gameID, int userID)
	{
		if (this.gd != null)
			yield break;

		string link = "https://localhost:5001/Game/RemoveUserFromGame/" + gameID + "/" + userID;

		using (UnityWebRequest req = UnityWebRequest.Post(link, "POST"))
		{
			req.SetRequestHeader("accept", "*/*");

			req.SendWebRequest();

			while (!req.isDone)
			{

			}

			if (req.error != null)
			{
				LogMessage(req.error);
				yield break;
			}
		}
	}
}