// UnityAdsOnLoad.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
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
	private var _yieldTime : float = 1.0;

#if UNITY_IOS || UNITY_ANDROID
	// A return type of IEnumerator allows for the use of yield statements.
	//  For more info, see: http://docs.unity3d.com/ScriptReference/YieldInstruction.html
	function Start () : IEnumerator
	{
		// Zone name used in debug messages.
		var zoneName : String = String.IsNullOrEmpty(zoneID) ? "the default ad placement zone" : zoneID;

		// Set a start time for the timeout.
		_startTime = Time.timeSinceLevelLoad;
		
		// Check to see if Unity Ads is initialized.
		//  If not, wait a second before trying again.
		while (!UnityAdsHelper.isInitialized)
		{
			if (Time.timeSinceLevelLoad - _startTime > timeout)
			{
				Debug.LogWarning("Unity Ads failed to initialize in a timely manner. Ad will not be shown on load.");
				
				// Break out of both this loop and the Start method; Unity Ads will not
				//  be shown on load since the wait time exceeded the time limit.
				return;
			}
			
			yield WaitForSeconds(_yieldTime);
		}

		Debug.Log("Unity Ads has finished initializing. Waiting for ads to be ready...");

		// Set a start time for the timeout.
		_startTime = Time.timeSinceLevelLoad;
		
		// Check to see if Unity Ads are available and ready to be shown. 
		//  If not, wait a second before trying again.
		while (!UnityAdsHelper.isReady(zoneID))
		{
			if (Time.timeSinceLevelLoad - _startTime > timeout)
			{
				Debug.LogWarning(String.Format("The process of showing ads on load for {0} has timed out. Ad was not shown.",zoneName));

				// Break out of both this loop and the Start method; Unity Ads will not
				//  be shown on load since the wait time exceeded the time limit.
				return;
			}

			yield WaitForSeconds(_yieldTime);
		}
		
		Debug.Log(String.Format("Ads for {0} are available and ready. Showing ad now...",zoneName));
		
		// Show ad after Unity Ads finishes initializing and ads are ready to show.
		UnityAdsHelper.ShowAd(zoneID,!disablePause);
	}
#endif
}
