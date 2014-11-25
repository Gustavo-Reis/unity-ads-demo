using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnityAdsInfo : MonoBehaviour 
{
	public bool showTimeScale = true;
	public bool showMouseStatus = true;

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space)) ShowInfo();
	}

	private void ShowInfo ()
	{
		List<string> info = new List<string>();

		if (showTimeScale) info.Add(ShowTimeScale());
		if (showMouseStatus) info.Add(ShowMouseStatus(2));

		if (info.Count > 0) Debug.Log(Concat(info,"\n"));
	}

	public static string Concat (List<string> info, string delimiter)
	{
		string result = string.Empty;

		for (int i = 0; i < info.Count; i++)
		{
			result += string.Concat(i>0?delimiter:string.Empty,info[i]);
		}

		return result;
	}

	public static string ShowTimeScale ()
	{
		return string.Format("Time Scale: {0}",Time.timeScale);
	}

	public static string ShowMouseStatus (int buttonCount = 1)
	{
		List<string> status = new List<string>();
		bool isMoving = Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0; 

		for (int i = 0; i < buttonCount; ++i)
		{
			status.Add(GetMouseButtonStatus(i));
		}

		status.Add(string.Concat("Mouse is ",isMoving?"moving":"idle"));

		return string.Format("Mouse Status: {0}",Concat(status,", "));
	}

	private static string GetMouseButtonStatus (int button = 1)
	{
		string format = "Button {0} {1}"; 
		string result = string.Format(format,button,"not pressed");;

		if (Input.GetMouseButtonDown(button))
			result = string.Format(format,button,"was pressed");
		else if (Input.GetMouseButtonUp(button))
			result = string.Format(format,button,"was released");
		else if (Input.GetMouseButton(button))
			result = string.Format(format,button,"still pressed");

		return result;
	}
}
