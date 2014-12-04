// UnityAdsHelperExt.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// Component extension for UnityAdsHelper that implements Server-to-Server Redeem Callback functionality.
//  http://unityads.unity3d.com/help/Documentation%20for%20Publishers/Server-to-server-Redeem-Callbacks

using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine.Advertisements.Optional;

[RequireComponent(typeof(UnityAdsHelper))]
public class UnityAdsHelperExt : MonoBehaviour 
{
	public string s2sRedeemUserID = string.Empty;

	private static string _sid;

	void Awake ()
	{
		if (string.IsNullOrEmpty(s2sRedeemUserID)) 
			s2sRedeemUserID = SystemInfo.deviceUniqueIdentifier;
		_sid = s2sRedeemUserID;
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
}