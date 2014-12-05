// UnityAdsHelper.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// Setup Instructions:
// 1. Attach this script to a new game object.
// 2. Enter game IDs into the fields provided.
// 3. Enable Development Build in Build Settings to 
//     enable test mode and show SDK debug levels.
// 
// Usage Guide:
//  Write a script and call UnityAdsHelper.ShowAd() to show an ad. 
//  Customize the HandleShowResults method to perform actions based 
//  on whether an ad was succesfully shown or not.
//
// Notes:
//  - Game IDs by platform are required to initialize Unity Ads.
//  - Test game IDs are optional. If not set while in test mode, 
//     test game IDs will default to platform game IDs.
//  - The various debug levels and test mode are only used when
//     Development Build is enabled in Build Settings.
//  - Test mode can be disabled while Development Build is set
//     by checking the option to disable it in the inspector.

#pragma strict
#if UNITY_IOS || UNITY_ANDROID
import UnityEngine.Advertisements;
#endif

public class UnityAdsHelper extends MonoBehaviour
{
	@System.Serializable
	public class GameInfo
	{
		@SerializeField
		private var _gameID : String;
		@SerializeField
		private var _testGameID : String;

		public function GetGameID () : String
		{
			return Debug.isDebugBuild && !String.IsNullOrEmpty(_testGameID) ? _testGameID : _gameID;
		}
	}
	public var iOS : GameInfo;
	public var android : GameInfo;
	public var disableTestMode : boolean;
	public var showInfoLogs : boolean;
	public var showDebugLogs : boolean;
	public var showWarningLogs : boolean = true;
	public var showErrorLogs : boolean = true;

#if UNITY_IOS || UNITY_ANDROID
	protected function Awake () : void
	{
		var gameID : String = null;

#if UNITY_IOS
		gameID = iOS.GetGameID();
#elif UNITY_ANDROID
		gameID = android.GetGameID();
#endif

		if (!Advertisement.isSupported)
		{
			Debug.LogWarning("Unity Ads is not supported on the current platform.");
		}
		else if (String.IsNullOrEmpty(gameID))
		{
			Debug.LogError("A valid game ID is required to initialize Unity Ads.");
		}
		else
		{
			Advertisement.allowPrecache = true;

			Advertisement.debugLevel = Advertisement.DebugLevel.NONE;
			if (showInfoLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.INFO;
			if (showDebugLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.DEBUG;
			if (showWarningLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.WARNING;
			if (showErrorLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.ERROR;

			var enableTestMode : boolean = Debug.isDebugBuild && !disableTestMode;
			Debug.Log(String.Format("Initializing Unity Ads for game ID {0} with test mode {1}...",
				                       gameID, enableTestMode ? "enabled" : "disabled"));

			Advertisement.Initialize(gameID,enableTestMode);
		}
	}

	public static function isReady () : boolean { return isReady(null); }
	public static function isReady (zone : String) : boolean
	{
		if (String.IsNullOrEmpty(zone)) zone = null;
		return Advertisement.isReady(zone);
	}

	public static function ShowAd () : void { ShowAd(null,true); }
	public static function ShowAd (zone : String) : void { ShowAd(zone,true); }
	public static function ShowAd (zone : String, pauseGameDuringAd : boolean) : void
	{
		if (String.IsNullOrEmpty(zone)) zone = null;
		
		var options : ShowOptions = new ShowOptions();
		options.pause = pauseGameDuringAd;
		options.resultCallback = HandleShowResult;

		Advertisement.Show(zone,options);
	}

	public static function HandleShowResult (result : ShowResult) : void
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
#else
	protected function Awake () : void
	{
		Debug.Log("Unity Ads is not supported on the current platform.");
	}
#endif
}
