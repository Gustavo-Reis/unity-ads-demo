using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class UnityAdsButton : MonoBehaviour 
{
	public string zone = string.Empty;

	void Update ()
	{
		collider.enabled = Advertisement.isReady(zone);
	}

	void OnMouseUpAsButton ()
	{
		UnityAdsDemoRobust.ShowAd(zone);
	}
}