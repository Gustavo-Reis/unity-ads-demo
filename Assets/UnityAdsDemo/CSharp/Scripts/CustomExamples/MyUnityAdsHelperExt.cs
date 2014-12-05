// MyUnityAdsHelperExt.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// An example of how to write your own custom helper script using inheritance.

using UnityEngine;
using System.Collections;
#if UNITY_IOS || UNITY_ANDROID
using UnityEngine.Advertisements;
using UnityEngine.Advertisements.Optional;
#endif

[RequireComponent(typeof(MyUnityAdsHelper))]
public class MyUnityAdsHelperExt : UnityAdsHelperExt 
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
		
		ShowOptionsExtended options = new ShowOptionsExtended();
		options.gamerSid = _sid;
		options.pause = pauseGameDuringAd;
		
		if (_doHandleByZone)
		{
			switch(zone)
			{
			case "rewardedVideoZone":
			case "incentivisedVideoZone":
				options.resultCallback = MyUnityAdsHelper.HandleShowResultAndReward;
				break;
			default:
				options.resultCallback = MyUnityAdsHelper.HandleShowResult;
				break;
			}
			
		}
		else
		{
			options.resultCallback = MyUnityAdsHelper.HandleShowResult;
		}

		Advertisement.Show(zone,options);
	}
#endif
}
