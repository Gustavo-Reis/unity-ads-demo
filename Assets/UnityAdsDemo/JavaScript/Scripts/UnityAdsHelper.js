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
//
// HACK Notes:
//  - Enable usePauseOverride if your game fails to unpause 
//     or gets stuck on a black screen after an ad is closed.

#pragma strict
import UnityEngine.Advertisements;

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
public var usePauseOverride : boolean; // HACK: Workaround for pause/resume bug.
public var showInfoLogs : boolean;
public var showDebugLogs : boolean;
public var showWarningLogs : boolean = true;
public var showErrorLogs : boolean = true;

private static var _isPaused : boolean; // HACK: Workaround for pause/resume bug.

function Awake () : void
{
	var gameID : String = null;

#if UNITY_IOS
	gameID = iOS.GetGameID();
#elif UNITY_ANDROID
	gameID = android.GetGameID();
#endif

	if (!Advertisement.isSupported)
	{
		Debug.Log("Current platform is not supported with Unity Ads.");
	}
	else if (String.IsNullOrEmpty(gameID))
	{
		Debug.Log("A valid game ID is required to initialize Unity Ads.");
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

// HACK: Workaround for pause/resume bug. See Hack Notes above for details.
function OnApplicationPause (isPaused : boolean) : void
{
	if (!usePauseOverride || isPaused == _isPaused) return;

	if (isPaused) Debug.Log ("App was paused.");
	else Debug.Log("App was resumed.");

	if (usePauseOverride) PauseOverride(isPaused);
}


public static function isReady (zone : String) : boolean
{
	if (String.IsNullOrEmpty(zone)) zone = null;
	return Advertisement.isReady(zone);
}

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

// HACK: Workaround for pause/resume bug. See Hack Notes above for details.
public static function PauseOverride (pause : boolean) : void
{
	if (pause) Debug.Log("Pause game while ad is shown.");
	else Debug.Log("Resume game after ad is closed.");
	
	AudioListener.volume = pause ? 0f : 1f;
	Time.timeScale = pause ? 0f : 1f;

	_isPaused = pause;
}