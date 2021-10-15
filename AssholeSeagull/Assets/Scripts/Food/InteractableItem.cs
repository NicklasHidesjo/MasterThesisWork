using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// check if this script is in use (currently only used by BottomBun) and remove if not
// BottomBun might not be in use either.
public class InteractableItem : MonoBehaviour
{
	public Vector3 Velocity => body.velocity;

	bool inHand;

	public bool InHand => inHand;

	Rigidbody body;

	private void Start()
	{
		body = GetComponent<Rigidbody>();
	}

	public void PickUp(GameObject picker)
	{
		Destroy(GetComponent<Rigidbody>());
		inHand = true;
		transform.SetParent(picker.transform, true);
	}
	public void DropItem(Vector3 force)
	{
		transform.SetParent(null, true);
		gameObject.AddComponent<Rigidbody>();

		inHand = false;

		body = GetComponent<Rigidbody>();
		body.AddForce(force, ForceMode.Impulse);
	}
}
