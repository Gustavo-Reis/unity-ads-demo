// UnityAdsDemoSimple.cs
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A super simple Unity Ads demo script; the very minimum necessary
//  to show Unity Ads in your game. This example uses the settings 
//  provided in your default Ad Placement zone. 
//
// From the Unity Ads Admin, make sure that the Monetization option 
//  for your game is enabled, and that at least one Ad Placement zone 
//  is set as default and enabled.
//
// Instructions:
// 1. Create a new Scene in Unity.
// 2. Create an new empty GameObject and rename it to Unity Ads Demo.
// 3. Attach this script to your GameObject called Unity Ads Demo.
// 4. With your Unity Ads Demo GameObject selected, enter your game ID 
//     and enable the Test Mode option in the Inspector panel.
// 5. Build and Run this scene on your target device.
//
// Results:
// On application start, an advertisement should appear.

using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsDemoSimple : MonoBehaviour 
{
	public string gameId;
	public bool testMode;
	
	// In order to use the yield statement in the Start() method, 
	//  specify a return type of IEnumerator instead of void.
	IEnumerator Start ()
	{
		// Make sure a game ID is provided.
		if (string.IsNullOrEmpty(gameId))
		{
			Debug.LogError("Unable to initialize Unity Ads without a valid game ID.");
			yield break;
		}

		Debug.Log(string.Format("Initializing Unity Ads for game ID {0} with test mode {1}...",
		          gameId, testMode ? "enabled" : "disabled"));

		Advertisement.Initialize(gameId,testMode);

		// Before attempting to show an ad, we should first check that
		//  Unity Ads has completed the initialization process, 
		//  and that ads are both available and ready.
		// 
		// Check to see if Unity Ads is initialized.
		//  If not, wait until the next frame and check again.
		do yield return null;
		while (!Advertisement.isInitialized);

		Debug.Log ("Unity Ads has finished initializing. Waiting for ads to be ready...");

		// Check to see if Unity Ads are available and ready to be shown. 
		//  If not, wait until the next frame and check again.
		do yield return null;
		while (!Advertisement.isReady());
		
		Debug.Log ("Ads are available and ready. Showing ad now...");

		// Once Unity Ads finishes initializing, we show an ad.
		Advertisement.Show();
	}
}
