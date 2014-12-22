// UnityAdsOnLoad.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A simple example for showing ads on load using the UnityAdsHelper script.

using UnityEngine;
using System.Collections;

public class UnityAdsOnLoad : MonoBehaviour 
{
	public string zoneID = string.Empty;
	public float timeout = 15f;
	public bool disablePause;

	private float _startTime = 0f;
	private float _yieldTime = 1f;

#if UNITY_IOS || UNITY_ANDROID
	IEnumerator Start ()
	{
		// Check to see if Unity Ads is initialized.
		//  If not, wait a second before trying again.
		do yield return new WaitForSeconds(_yieldTime);
		while (!UnityAdsHelper.isInitialized);
		
		Debug.Log("Unity Ads has finished initializing. Waiting for ads to be ready...");

		// Set a start time for the timeout.
		_startTime = Time.timeSinceLevelLoad;
		
		// Check to see if Unity Ads are available and ready to be shown. 
		//  If not, wait a second before trying again.
		while (!UnityAdsHelper.isReady(zoneID))
		{
			if (Time.timeSinceLevelLoad - _startTime > timeout)
			{
				Debug.LogWarning("The process for showing ads on load has timed out. Ad not shown.");

				// Break out of both this loop and the Start method; Unity Ads will not
				//  be shown on load since the wait time exceeded the time limit.
				yield break;
			}

			yield return new WaitForSeconds(_yieldTime);
		}
		
		Debug.Log("Ads are available and ready. Showing ad now...");
		
		// Show ad after Unity Ads finishes initializing and ads are ready to show.
		UnityAdsHelper.ShowAd(zoneID,!disablePause);
	}
#endif
}
