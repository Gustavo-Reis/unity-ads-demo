// UnityAdsButton.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A simple example for showing ads on load using the UnityAdsHelper script.

#pragma strict

public class UnityAdsOnLoad extends MonoBehaviour 
{
	public var zoneID : String = String.Empty;
	public var timeout : float = 15.0;
	public var disablePause : boolean;

	private var _startTime : float = 0.0;

#if UNITY_IOS || UNITY_ANDROID
	function Start () : IEnumerator
	{
		// Check to see if Unity Ads is initialized.
		//  If not, wait a second before trying again.
		do { yield WaitForSeconds(1.0); }
		while (!UnityAdsHelper.isInitialized());
		
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
				return;
			}

			yield WaitForSeconds(1.0);
		}
		
		Debug.Log("Ads are available and ready. Showing ad now...");
		
		// Show ad after Unity Ads finishes initializing and ads are ready to show.
		UnityAdsHelper.ShowAd(zoneID,!disablePause);
	}
#endif
}
