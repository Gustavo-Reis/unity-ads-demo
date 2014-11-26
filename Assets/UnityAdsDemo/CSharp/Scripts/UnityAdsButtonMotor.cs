// UnityAdsButtonMotor.cs - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A case specific behavior controller for the UnityAdsButton example.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UnityAdsButton))]
public class UnityAdsButtonMotor : MonoBehaviour 
{
	public float rotationSpeed = -5f;
	public float disabledPosZ = 10f;
	public float tweenSpeed = 1f;

	private Vector3 _initialPos = Vector3.zero;
	private Vector3 _disabledPos = Vector3.zero;

	private UnityAdsButton _button;

	void Start ()
	{
		_button = GetComponent<UnityAdsButton>();

		_initialPos = transform.localPosition;
		_disabledPos = new Vector3(_initialPos.x,_initialPos.y,disabledPosZ);

		transform.localPosition = _disabledPos;
	}

	void Update ()
	{
		if (UnityAdsHelper.isReady(_button.zoneID))
		{
			collider.enabled = true;

			Vector3 rotation = transform.localRotation.eulerAngles;
			rotation += Vector3.up * rotationSpeed * Time.deltaTime;

			transform.localRotation = Quaternion.Euler(rotation);
			transform.localPosition = MoveTowards(_initialPos);
		}
		else
		{
			collider.enabled = false;

			transform.localPosition = MoveTowards(_disabledPos);
		}
	}

	private Vector3 MoveTowards (Vector3 targetPos)
	{
		float step = Vector3.Magnitude(_disabledPos - _initialPos) / tweenSpeed * Time.deltaTime;
		return Vector3.MoveTowards(transform.localPosition, targetPos, step);
	}
}