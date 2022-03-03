using System;

[Serializable]
public class UserData
{
	public int id;
	public string username;
	public string password;
	public string salt;
	public string tag;
	public string firstName;
	public string lastName;

	public override string ToString()
	{
		return UnityEngine.JsonUtility.ToJson(this, true);
	}
}
