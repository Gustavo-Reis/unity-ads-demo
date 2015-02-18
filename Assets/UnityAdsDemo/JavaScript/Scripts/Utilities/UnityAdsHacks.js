// UnityAdsHacks.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// HACK 1: Unity Ads fails to call ResumeGame when an ad is closed, resulting in the 
//          game remaining indefinitely in a pause state or being stuck on a black screen.
//       - The UnityAds.PauseGame method sets Time.timeScale and AudioListener.volume to 0.
//       - Set these values back to 1 manually within the OnApplicaitonPause method.
//       - Enable usePauseOverride in the inspector to use this workaround.

#pragma strict

public class UnityAdsHacks extends MonoBehaviour 
{
	public var usePauseOverride : boolean;

	protected static var _isPaused : boolean;
	
	protected function OnApplicationPause (isPaused : boolean) : void
	{
		if (isPaused == _isPaused) return;

		if (usePauseOverride) PauseOverride(isPaused);
		else if (isPaused) Debug.Log ("App was paused (sent to background).");
		else Debug.Log("App was resumed (returned from background).");

		_isPaused = isPaused;
	}

	public static function PauseOverride (pause : boolean) : void
	{
		var targetValue : float = pause ? 0.0 : 1.0;
		var targetValueMsg : String = String.Format("(volume and timeScale value set to {0})",targetValue);
	
		AudioListener.volume = targetValue;
		Time.timeScale = targetValue;

		if (pause) Debug.Log(String.Format("UNITY ADS HACK: Pause game while ad is shown {0}.",targetValueMsg));
		else Debug.Log(String.Format("UNITY ADS HACK: Resume game after ad is closed {0}.",targetValueMsg));		
	}
}
