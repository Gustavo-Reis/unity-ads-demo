// UnityAdsHelperGUI.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityAdsHelper))]
public class UnityAdsHelperGUI : MonoBehaviour 
{
	public string zoneID = string.Empty;
	public bool disablePause;

	void OnGUI ()
	{
		bool isReady = UnityAdsHelper.isReady(zoneID);
		bool pause = !disablePause;
		
		GUI.enabled = isReady;
		if (GUI.Button(new Rect(10, 10, 150, 50), isReady ? "Show Ad" : "Waiting...")) 
		{
			Debug.Log(string.Format("Ad Placement zone with ID of {0} is {1}.",
			                        string.IsNullOrEmpty(zoneID) ? "null" : zoneID,
			                        isReady ? "ready" : "not ready"));
			
			if (isReady) UnityAdsHelper.ShowAd(zoneID,pause);
		}
		GUI.enabled = true;
	}
}
