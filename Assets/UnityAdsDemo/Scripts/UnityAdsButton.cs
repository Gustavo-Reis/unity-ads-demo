using UnityEngine;
using System.Collections;

public class UnityAdsButton : MonoBehaviour 
{
	public string zone = string.Empty;

	void OnMouseDown ()
	{
		UnityAdsDemoRobust.ShowAd(zone);
	}
}