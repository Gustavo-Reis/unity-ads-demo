// MyUnityAdsHelperExt.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// An example of how to write your own custom helper script using inheritance.

#pragma strict
#if UNITY_IOS || UNITY_ANDROID
import UnityEngine.Advertisements;
#endif

@script RequireComponent(MyUnityAdsHelper)
public class MyUnityAdsHelperExt extends UnityAdsHelperExt 
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
		
		var options : ShowOptionsExtended = new ShowOptionsExtended();
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
