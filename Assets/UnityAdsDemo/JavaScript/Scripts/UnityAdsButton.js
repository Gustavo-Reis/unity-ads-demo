// UnityAdsButton.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A simple button example for showing Unity Ads.

#pragma strict

public class UnityAdsButton extends MonoBehaviour
{
	public var zoneID : String = String.Empty;
	public var disablePause : boolean;

#if UNITY_IOS || UNITY_ANDROID
	function OnMouseUpAsButton () : void
	{
		if (UnityAdsHelper.isReady(zoneID))
		{
			UnityAdsHelper.ShowAd(zoneID,!disablePause);
		}
		else
		{
			Debug.LogWarning("Unable to show ad. Zone is not yet ready.");
		}
	}
#endif
}
