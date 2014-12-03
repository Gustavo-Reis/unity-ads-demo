// UnityAdsHelperExtGUI.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityAdsHelperExt))]
public class UnityAdsHelperExtGUI : MonoBehaviour 
{
	public string s2sRedeemZoneID = "rewardedVideoZone";
	public bool disablePause;

	void OnGUI ()
	{
		bool isReady = UnityAdsHelper.isReady(s2sRedeemZoneID);
		bool pause = !disablePause;
		
		GUI.enabled = isReady;
		if (GUI.Button(new Rect(Screen.width-310, 10, 300, 50), isReady ? "Show Ad With S2S Redeem Callback" : "Waiting...")) 
		{
			Debug.Log(string.Format("Ad Placement zone with ID of {0} is {1}. This zone is using S2S Redeem Callback functionality.",
			                        string.IsNullOrEmpty(s2sRedeemZoneID) ? "null" : s2sRedeemZoneID,
			                        isReady ? "ready" : "not ready"));
			
			if (isReady) UnityAdsHelperExt.ShowAd(s2sRedeemZoneID,pause);
		}
		GUI.enabled = true;
	}
}
