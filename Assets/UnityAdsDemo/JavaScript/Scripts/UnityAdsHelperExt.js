// UnityAdsHelperExt.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// Component extension for UnityAdsHelper that implements Server-to-Server Redeem Callback functionality.
//  http://unityads.unity3d.com/help/Documentation%20for%20Publishers/Server-to-server-Redeem-Callbacks

#pragma strict
#if UNITY_IOS || UNITY_ANDROID
import UnityEngine.Advertisements;
import UnityEngine.Advertisements.Optional;
#endif

@script RequireComponent(UnityAdsHelper)
public class UnityAdsHelperExt extends MonoBehaviour
{
	public var s2sRedeemUserID : String = String.Empty;

	protected private static var _sid : String;

	protected function Awake () : void
	{
		_sid = s2sRedeemUserID = GetValidSID(s2sRedeemUserID);
	}

	public static function ShowAd () : boolean { return ShowAd(null,true); }
	public static function ShowAd (zone : String) : boolean { return ShowAd(zone,true); }
	public static function ShowAd (zone : String, pauseGameDuringAd : boolean) : boolean
	{
#if UNITY_IOS || UNITY_ANDROID
		if (String.IsNullOrEmpty(zone)) zone = null;
		
		if (!Advertisement.isReady(zone))
		{
			Debug.LogWarning(String.Format("Unable to show ad. The ad placement zone ($0) is not ready.",
			                               zone == null ? "default" : zone));
			return false;
		}

		var options : ShowOptionsExtended = new ShowOptionsExtended();
		options.gamerSid = _sid;
		options.pause = pauseGameDuringAd;
		options.resultCallback = HandleShowResult;

		Advertisement.Show(zone,options);
		
		return true;
#else
		Debug.LogError("Failed to show ad. Unity Ads is not supported on the current build platform.");
		return false;
#endif
	}

	public static function GetValidSID (id : String)
	{
		return (String.IsNullOrEmpty(id)) ? SystemInfo.deviceUniqueIdentifier : id;
	}

#if UNITY_IOS || UNITY_ANDROID
	private static function HandleShowResult (result : ShowResult) : void
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			break;
		case ShowResult.Skipped:
			Debug.LogWarning("The ad was skipped before reaching the end.");
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			break;
		}
	}
#endif
}
