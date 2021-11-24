using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZRotator : MonoBehaviour
{
    [Tooltip("The total rotation that we want our object to make on the Z-axis")]
	[SerializeField] int goalRotation = 360;

	[SerializeField] GameObject parent;
    
    float timer;
    float rotationTime;

    void Start()
	{
        if (GameManager.Settings.TimerOff)
        {
			parent.SetActive(false);
        }
		
		// make sure our timer is at 0
		timer = 0;

		SetRotationTime();
	}

	private void SetRotationTime()
	{
		// set the rotation time using the GameDuration in the GameManager.
		rotationTime = GameManager.Settings.GameDuration;

	}

	void Update()
	{
		if(GameManager.CurrentGameStatus != GameStatus.ingame)
        {
			return;
        }
		// increase the time that has passed since we started our rotation.
		timer += Time.deltaTime / rotationTime;

		Rotate();
	}

	private void Rotate()
	{
		// get the current rotation of our object.
		Vector3 rotation = transform.rotation.eulerAngles;
		
		// calculate the rotation that we should have on the Z-axis.
		float zRotation = Mathf.Lerp(0, goalRotation, timer);

		// modify our objects stored rotation on the Z-axis.
		rotation.z = zRotation;

		// apply our new rotation with modified Z value to our object.
		transform.eulerAngles = rotation;
	}
}
