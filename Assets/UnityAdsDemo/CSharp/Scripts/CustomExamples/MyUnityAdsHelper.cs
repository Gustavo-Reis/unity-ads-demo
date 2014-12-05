// MyUnityAdsHelper.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// An example of how to write your own custom helper script using inheritance.

using UnityEngine;
using System.Collections;
#if UNITY_IOS || UNITY_ANDROID
using UnityEngine.Advertisements;
#endif

public class MyUnityAdsHelper : UnityAdsHelper
{
	public bool handleByZone;

	private static bool _doHandleByZone;

#if UNITY_IOS || UNITY_ANDROID
	new void Awake ()
	{
		base.Awake();

		_doHandleByZone = handleByZone;
	}

	public new static void ShowAd (string zone = null, bool pauseGameDuringAd = true)
	{
		if (string.IsNullOrEmpty(zone)) zone = null;
		
		ShowOptions options = new ShowOptions();
		options.pause = pauseGameDuringAd;

		if (_doHandleByZone)
		{
			switch(zone)
			{
			case "rewardedVideoZone":
			case "incentivisedVideoZone":
				options.resultCallback = HandleShowResultAndReward;
				break;
			default:
				options.resultCallback = HandleShowResult;
				break;
			}
		}
		else
		{
			options.resultCallback = HandleShowResult;
		}
		
		Advertisement.Show(zone,options);
	}

	public new static void HandleShowResult (ShowResult result)
	{
		UnityAdsHelper.HandleShowResult(result);

		LoadNextLevel();
	}

	public static void HandleShowResultAndReward (ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown. User shall be rewarded.");
			RewardUserForWatchingAdvertisement();
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end. User will not be rewarded.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown. User will not be rewarded.");
			break;
		}

		LoadNextLevel();
	}

	public static void LoadNextLevel ()
	{
		int indexOfNextLevel = Application.loadedLevel;
		if (indexOfNextLevel < Application.levelCount)
		{
			Debug.Log("Load next level.");
			Application.LoadLevel(indexOfNextLevel);
		}
		else
		{
			Debug.LogError("Failed to load next level. The index is out of bounds.");
		}
	}

	public static void ReloadLevel ()
	{
		Debug.Log("Reload current level.");
		Application.LoadLevel(Application.loadedLevel);
	}

	private static void RewardUserForWatchingAdvertisement ()
	{
		Debug.Log("User has been rewarded.");
	}
#endif
}
