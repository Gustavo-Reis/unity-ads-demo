using UnityEngine;
using System.Collections;

public class UnityAdsInfo : MonoBehaviour 
{
	public bool showTimeScale;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space)) ShowInfo();
	}

	private void ShowInfo ()
	{
		string info = string.Empty;

		if (showTimeScale) info += "Time Scale: " + Time.timeScale;

		if (!string.IsNullOrEmpty(info)) Debug.Log(info);
	}
}
