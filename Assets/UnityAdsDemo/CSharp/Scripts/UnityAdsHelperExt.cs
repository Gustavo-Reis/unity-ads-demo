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

#if UNITY_IOS || UNITY_ANDROID
	protected void Awake ()
	{
		_sid = s2sRedeemUserID = GetValidSID(s2sRedeemUserID);
	}

	public static void ShowAd (string zone = null, bool pauseGameDuringAd = true)
	{
		if (string.IsNullOrEmpty(zone)) zone = null;
		
		ShowOptionsExtended options = new ShowOptionsExtended();
		options.gamerSid = _sid;
		options.pause = pauseGameDuringAd;
		options.resultCallback = UnityAdsHelper.HandleShowResult;

		Advertisement.Show(zone,options);
	}

	public static string GetValidSID (string id)
	{
		return (string.IsNullOrEmpty(id)) ? SystemInfo.deviceUniqueIdentifier : id;
	}
#endif
}
