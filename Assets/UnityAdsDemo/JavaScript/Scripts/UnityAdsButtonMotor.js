// UnityAdsButtonMotor.js - Written for Unity Ads Asset Store v1.0.4 (SDK 1.3.10)
//  by Nikkolai Davenport <nikkolai@unity3d.com>
//
// A case specific behavior controller for the UnityAdsButton example.

#pragma strict

@script RequireComponent(UnityAdsButton)

public var rotationSpeed : float = -5.0;
public var disabledPosZ : float = 10.0;
public var tweenSpeed : float = 1.0;

private var _initialPos : Vector3 = Vector3.zero;
private var _disabledPos : Vector3 = Vector3.zero;

private var _button : UnityAdsButton;

function Start () : void
{
	_button = GetComponent(UnityAdsButton);

	_initialPos = transform.localPosition;
	_disabledPos = new Vector3(_initialPos.x,_initialPos.y,disabledPosZ);

	transform.localPosition = _disabledPos;
}

function Update () : void
{
	if (UnityAdsHelper.isReady(_button.zoneID))
	{
		collider.enabled = true;

		var rotation : Vector3 = transform.localRotation.eulerAngles;
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

private function MoveTowards (targetPos : Vector3) : Vector3
{
	var step : float = Vector3.Magnitude(_disabledPos - _initialPos) / tweenSpeed * Time.deltaTime;
	return Vector3.MoveTowards(transform.localPosition, targetPos, step);
}