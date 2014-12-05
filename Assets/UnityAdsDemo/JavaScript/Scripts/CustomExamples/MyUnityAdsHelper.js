// MyUnityAdsHelper.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// An example of how to write your own custom helper script using inheritance.

#pragma strict
#if UNITY_IOS || UNITY_ANDROID
import UnityEngine.Advertisements;
#endif

public class MyUnityAdsHelper extends UnityAdsHelper
{
	public var handleByZone : boolean;

	private static var _doHandleByZone : boolean;

#if UNITY_IOS || UNITY_ANDROID
	function Awake () : void
	{
		super.Awake();

		_doHandleByZone = handleByZone;
	}

	public static function ShowAd (zone : String, pauseGameDuringAd : boolean) : void
	{
		if (String.IsNullOrEmpty(zone)) zone = null;
		
		var options : ShowOptions = new ShowOptions();
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

	public static function HandleShowResult (result : ShowResult) : void
	{
		UnityAdsHelper.HandleShowResult(result);

		LoadNextLevel();
	}

	public static function HandleShowResultAndReward (result : ShowResult) : void
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

	public static function LoadNextLevel () : void
	{
		var indexOfNextLevel : int = Application.loadedLevel;
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

	public static function ReloadLevel () : void
	{
		Debug.Log("Reload current level.");
		Application.LoadLevel(Application.loadedLevel);
	}

	private static function RewardUserForWatchingAdvertisement () : void
	{
		Debug.Log("User has been rewarded.");
	}
#endif
}
