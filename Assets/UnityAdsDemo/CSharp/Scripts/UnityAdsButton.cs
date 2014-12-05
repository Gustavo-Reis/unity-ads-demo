// UnityAdsButton.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A simple button example for showing Unity Ads.

using UnityEngine;
using System.Collections;

public class UnityAdsButton : MonoBehaviour 
{
	public string zoneID = string.Empty;
	public bool disablePause;

#if UNITY_IOS || UNITY_ANDROID
	void OnMouseUpAsButton ()
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
