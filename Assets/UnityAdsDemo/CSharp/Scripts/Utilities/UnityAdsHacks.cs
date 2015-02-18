// UnityAdsHacks.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// HACK 1: Unity Ads fails to call ResumeGame when an ad is closed, resulting in the 
//          game remaining indefinitely in a pause state or being stuck on a black screen.
//       - The UnityAds.PauseGame method sets Time.timeScale and AudioListener.volume to 0.
//       - Set these values back to 1 manually within the OnApplicaitonPause method.
//       - Enable usePauseOverride in the inspector to use this workaround.

using UnityEngine;
using System.Collections;

public class UnityAdsHacks : MonoBehaviour 
{
	public bool usePauseOverride;

	protected static bool _isPaused;
	
	protected void OnApplicationPause (bool isPaused)
	{
		if (isPaused == _isPaused) return;

		if (usePauseOverride) PauseOverride(isPaused);
		else if (isPaused) Debug.Log ("App was paused (sent to background).");
		else Debug.Log("App was resumed (returned from background).");

		_isPaused = isPaused;
	}

	public static void PauseOverride (bool pause)
	{
		float targetValue = pause ? 0f : 1f;
		string targetValueMsg = string.Format("(volume and timeScale value set to {0})",targetValue);

		AudioListener.volume = targetValue;
		Time.timeScale = targetValue;

		if (pause) Debug.Log(string.Format("UNITY ADS HACK: Pause game while ad is shown {0}.",targetValueMsg));
		else Debug.Log(string.Format("UNITY ADS HACK: Resume game after ad is closed {0}.",targetValueMsg));		
	}
}
