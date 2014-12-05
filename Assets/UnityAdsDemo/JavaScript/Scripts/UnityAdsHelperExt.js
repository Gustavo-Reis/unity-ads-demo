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

public var s2sRedeemUserID : String = String.Empty;

private static var _sid : String;

#if UNITY_IOS || UNITY_ANDROID
function Awake () : void
{
	_sid = s2sRedeemUserID;
}

public static function ShowAd () : void { ShowAd(null,true); }
public static function ShowAd (zone : String) : void { ShowAd(zone,true); }
public static function ShowAd (zone : String, pauseGameDuringAd : boolean) : void
{
	if (String.IsNullOrEmpty(zone)) zone = null;
	if (String.IsNullOrEmpty(_sid)) _sid = SystemInfo.deviceUniqueIdentifier;
	
	var options : ShowOptionsExtended = new ShowOptionsExtended();
	options.gamerSid = _sid;
	options.pause = pauseGameDuringAd;
	options.resultCallback = UnityAdsHelper.HandleShowResult;

	Advertisement.Show(zone,options);
}
#endif
