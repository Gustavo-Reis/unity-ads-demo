// UnityAdsHelperExtGUI.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>

#pragma strict

@script RequireComponent(UnityAdsHelperExt)

public var s2sRedeemZoneID : String = "rewardedVideoZone";
public var disablePause : boolean;

function OnGUI () : void
{
	var isReady : boolean = UnityAdsHelper.isReady(s2sRedeemZoneID);
	var pause : boolean = !disablePause;
	
	GUI.enabled = isReady;
	if (GUI.Button(new Rect(Screen.width-310, 10, 300, 50), isReady ? "Show Ad With S2S Redeem Callback" : "Waiting...")) 
	{
		Debug.Log(String.Format("Ad Placement zone with ID of {0} is {1}. This zone is using S2S Redeem Callback functionality.",
		                        String.IsNullOrEmpty(s2sRedeemZoneID) ? "null" : s2sRedeemZoneID,
		                        isReady ? "ready" : "not ready"));
		
		if (isReady) UnityAdsHelperExt.ShowAd(s2sRedeemZoneID,pause);
	}
	GUI.enabled = true;
}