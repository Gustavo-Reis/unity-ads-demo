// UnityAdsButtonJS.js - Written for Unity Ads Asset Store v1.0.3 (SDK 1.3.9)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A simple button example for triggering Unity Ads.
//
// Prerequisites:
// 1) Add UnityAdsHelperJS to a new GameObject in your scene:
//     https://gist.github.com/wcoastsands/5ec2917916a957a6e8d0
//
// Instructions:
// 1) Add a Cube to your scene:
//     GameObject > Create Other > Cube
// 2) Set the position of the cube to (0,0,0).
// 3) Add this script to the cube.﻿

#pragma strict

public var zone : String = String.Empty;

function OnMouseUpAsButton () : void
{
	if (UnityAdsHelper.isReady(zone))
	{
		UnityAdsHelper.ShowAd(zone,true);
	}
}
