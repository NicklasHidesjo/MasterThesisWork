using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbyFeet : MonoBehaviour
{
    [SerializeField] private float attachForce = 800.0f;
    [SerializeField] private float attachForceDamper = 25.0f;

    Rigidbody foodRB = null;

	private void Update()
	{
		// change grabbyfeet to kinematic making this stuff useless

		if(foodRB == null)
		{
			return;
		}

		Vector3 targetPoint = foodRB.transform.TransformPoint(foodRB.position);
		Vector3 vdisplacement = foodRB.transform.position - targetPoint;

		foodRB.velocity = Vector3.zero;
		foodRB.angularVelocity = Vector3.zero;

		foodRB.AddForceAtPosition(attachForce * vdisplacement, targetPoint, ForceMode.Acceleration);
		foodRB.AddForceAtPosition(-attachForceDamper * foodRB.GetPointVelocity(targetPoint), targetPoint, ForceMode.Acceleration);

	}

	public void SetFoodRB(Rigidbody rigidBody)
	{
		foodRB = rigidBody;
	}
}
