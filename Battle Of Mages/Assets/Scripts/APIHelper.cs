
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEditor;

public class APIHelper
{
	public readonly string apiLink = "https://api.chucknorris.io/jokes/random";
	private void LogMessage(string message)
	{
#if UNITY_EDITOR
		EditorUtility.DisplayDialog("Title", message, "Ok");
#else
		Debug.Log(message);
#endif
	}

	public void Get()
	{
		RestClient.Get<string>(apiLink).Then(res =>
		{
			this.LogMessage(res);
		}).Catch(err => this.LogMessage(err.Message));

	}
}