// UnityAdsHelperGUI.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>

#pragma strict

@script RequireComponent(UnityAdsHelper)

public var zoneID : String = String.Empty;
public var disablePause : boolean;

function OnGUI () : void
{
	var isReady : boolean = UnityAdsHelper.isReady(zoneID);
	var pause : boolean = !disablePause;
	
	GUI.enabled = isReady;
	if (GUI.Button(new Rect(10, 10, 150, 50), isReady ? "Show Ad" : "Waiting...")) 
	{
		Debug.Log(String.Format("Ad Placement zone with ID of {0} is {1}.",
		                        String.IsNullOrEmpty(zoneID) ? "null" : zoneID,
		                        isReady ? "ready" : "not ready"));
		
		if (isReady) UnityAdsHelper.ShowAd(zoneID,pause);
	}
	GUI.enabled = true;
}