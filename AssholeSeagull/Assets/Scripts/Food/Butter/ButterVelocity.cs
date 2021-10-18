using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterVelocity : MonoBehaviour // rename this script to something better
{
    private float velocity;
    public float Velocity
    {
        get { return velocity; }
    }

    private Vector3 previousPos;

    void Start()
	{
		InitiatePreviousPos();
	}
	private void InitiatePreviousPos()
	{
		// make sure we have a previous position.
		Vector3 currentPos = transform.TransformPoint(transform.position);
		// set the previous position
		SetPreviousPos(currentPos);
	}

	void Update()
	{
		SetBladeVelocity();
	}

	private void SetBladeVelocity()
	{
		// get the current position of our object.
		Vector3 currentPos = transform.TransformPoint(transform.position);

		// get the distance traveled between our previous position and our current position
		float distanceTraveled = Vector3.Distance(previousPos, currentPos);

		// make sure that the distance traveled is a positive number,
		// as we only care about the velocity and not direction,
		// it's easier to have it always be positive. 
		distanceTraveled = Mathf.Abs(distanceTraveled);

		// convert our distance traveled into a velocity (m/s) 
		velocity = distanceTraveled / Time.deltaTime;

		// replace our previous position with the position we have right now.
		SetPreviousPos(currentPos);
	}
	private void SetPreviousPos(Vector3 position)
	{
		previousPos = position;
	}
}
