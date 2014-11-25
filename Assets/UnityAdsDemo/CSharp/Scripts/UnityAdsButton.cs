// UnityAdsButton.cs - Written for Unity Ads Asset Store v1.0.2 (SDK 1.3.8)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A simple button example for triggering Unity Ads.
//
// Prerequisites:
// 1) Add the UnityAdsHelper to a new GameObject in your scene:
//     https://gist.github.com/wcoastsands/9c1682579412efd49e32
//
// Instructions:
// 1) Add a Cube to your scene:
//     GameObject > Create Other > Cube
// 2) Set the position of the cube to (0,0,0).
// 3) Add this script to the cube.

using UnityEngine;

public class UnityAdsButton : MonoBehaviour 
{
	public string zone = string.Empty;
	public bool disablePause;

	void Update ()
	{
		// Enable the collider only when the zone isReady.
		//  Button is not clickable when zone is not ready.
		collider.enabled = UnityAdsHelper.isReady(zone);
	}

	void OnMouseUpAsButton ()
	{
		// Check to make sure the zone is ready before showing ad.
		if ( UnityAdsHelper.isReady(zone) ) 
		{
			UnityAdsHelper.ShowAd(zone,!disablePause);
		}
	}
}