// UnityAdsBandaid.cs - Written for Unity Ads Asset Store v1.0.2 (SDK 1.3.8)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A quick-fix script for providing workaround solutions to known issues.
// 
// Issues:
// 1) User stuck on black screen after closing Unity Ads advertisement.
//     This issue has an occurance rate of ~15%, and appears to be due to
//     the AudioListener.volume and Time.timeScale values being set to 0 
//     by Unity Ads, but not being set back to their original values 
//     after the ad is closed.

using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsBandaid : MonoBehaviour 
{
	public static void ShowAd (string zone = null)
	{
		AudioListener.volume = 0f;
		Time.timeScale = 0f;

		ShowOptions options = new ShowOptions();
		options.pause = false;
		options.resultCallback = HandleShowResult;
		
		Advertisement.Show(zone,options);
	}
	
	private static void HandleShowResult (ShowResult result)
	{
		AudioListener.volume = 1f;
		Time.timeScale = 1f;

		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
}