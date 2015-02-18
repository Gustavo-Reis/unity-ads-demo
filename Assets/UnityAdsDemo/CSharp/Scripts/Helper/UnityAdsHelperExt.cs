// UnityAdsHelperExt.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// Component extension for UnityAdsHelper that implements Server-to-Server Redeem Callback functionality.
//  http://unityads.unity3d.com/help/Documentation%20for%20Publishers/Server-to-server-Redeem-Callbacks

using UnityEngine;
using System.Collections;
#if UNITY_IOS || UNITY_ANDROID
using UnityEngine.Advertisements;
using UnityEngine.Advertisements.Optional;
#endif

[RequireComponent(typeof(UnityAdsHelper))]
public class UnityAdsHelperExt : MonoBehaviour 
{
	public string s2sRedeemUserID = string.Empty;

	protected static string _sid;

	protected void Awake ()
	{
		_sid = s2sRedeemUserID = GetValidSID(s2sRedeemUserID);
	}

	public static bool ShowAd (string zone = null, bool pauseGameDuringAd = true)
	{
#if UNITY_IOS || UNITY_ANDROID
		if (string.IsNullOrEmpty(zone)) zone = null;
		
		if (!Advertisement.isReady(zone))
		{
			Debug.LogWarning(string.Format("Unable to show ad. The ad placement zone ($0) is not ready.",
			                               zone == null ? "default" : zone));
			return false;
		}
		
		ShowOptionsExtended options = new ShowOptionsExtended();
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

	public static string GetValidSID (string id)
	{
		return (string.IsNullOrEmpty(id)) ? SystemInfo.deviceUniqueIdentifier : id;
	}

#if UNITY_IOS || UNITY_ANDROID
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
#endif
}
