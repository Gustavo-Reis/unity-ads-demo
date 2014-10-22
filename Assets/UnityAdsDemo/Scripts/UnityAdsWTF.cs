using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdsWTF : MonoBehaviour 
{
	[Serializable]
	public struct GameInfo
	{
		public string gameID;
		public string testGameID;

		public string GetGameID ()
		{
			return Debug.isDebugBuild && !string.IsNullOrEmpty(testGameID) ? testGameID : gameID;
		}
	}

	public GameInfo iOS;
	public GameInfo android;
	public string zoneID;

	public bool disableTestMode;

	public bool showInfoLogs;
	public bool showDebugLogs;
	public bool showWarningLogs = true;
	public bool showErrorLogs = true;

	void Awake () 
	{
		if (Advertisement.isSupported) 
		{
			string gameID = null;

#if UNITY_IOS
			gameID = iOS.GetGameID();
#elif UNITY_ANDROID
			gameID = android.GetGameID();
#endif
			if (string.IsNullOrEmpty(gameID))
			{
				Debug.LogError("A valid game ID is required to initialize Unity Ads.");
			}
			else
			{
				Advertisement.debugLevel |= Advertisement.DebugLevel.NONE;	

				if (showInfoLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.INFO;
				if (showDebugLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.DEBUG;
				if (showWarningLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.WARNING;
				if (showErrorLogs) Advertisement.debugLevel |= Advertisement.DebugLevel.ERROR;

				bool enableTestMode = Debug.isDebugBuild && !disableTestMode; 
				Debug.Log(string.Format("Initializing Unity Ads for game ID {0} with test mode {1}...",
				                        gameID, enableTestMode ? "enabled" : "disabled"));
				
				Advertisement.Initialize(gameID,enableTestMode);
			}
		} 
		else 
		{
			Debug.Log("Current platform is not supported with Unity Ads.");
		}
	}
	
	void OnGUI ()
	{
		if (Advertisement.isInitialized)
		{
			string zone = string.IsNullOrEmpty(zoneID) ? null : zoneID;
			bool isReady = Advertisement.isReady(zone);
			
			GUI.enabled = isReady;
			if (GUI.Button(new Rect(10, 10, 150, 50), isReady ? "Show Ad" : "Waiting...")) 
			{
				Debug.Log(string.Format("Ad Placement zone with ID of {0} is {1}.",
				                        string.IsNullOrEmpty(zone) ? "null" : zone,
				                        isReady ? "ready" : "not ready"));
				
				if (isReady) DoShowAd(zone);
			}
			GUI.enabled = true;
		}
		else
		{
			GUI.Label(new Rect(10, 10, 300, 50), "Unity Ads is not initialized.");
		}
	}

	public static void ShowAd (string zone = null)
	{
		if (Advertisement.isInitialized)
		{
			if (Advertisement.isReady(zone)) DoShowAd(zone);
			else Debug.LogWarning("Unable to show advertisement. Placement zone is not ready.");
		}
		else Debug.LogError("Failed to show advertisement. Unity Ads is not initialized.");
	}

	private static void DoShowAd (string zone = null, int version = 0)
	{
		switch (version)
		{
		case 0:
			DoShowAd_CondensedVersion(zone);
			break;
		case 1:
			DoShowAd_ExpandedVersion(zone);
			break;
		}
	}

	private static void DoShowAd_CondensedVersion (string zone, bool pauseGameDuringAd = true)
	{
		Advertisement.Show(zone, new ShowOptions {
			pause = pauseGameDuringAd,
			resultCallback = result => {
				HandleShowResult(result);
			}
		});
	}
	
	private static void DoShowAd_ExpandedVersion (string zone, bool pauseGameDuringAd = true)
	{
		ShowOptions options = new ShowOptions();
		options.pause = pauseGameDuringAd;
		options.resultCallback = HandleShowResult;
		
		Advertisement.Show(zone,options);
	}

	private static void HandleShowResult (ShowResult result)
	{
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