using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRespawner : MonoBehaviour
{
    // This is the transform that we want our knife to measure its out of bounds radius.
    [SerializeField] Transform centerTransform;

    // this is the reach that we deem the player to have
    [SerializeField] float playerReach = 10f;

    // this is the position of our center (comes form centerTransform)
    Vector3 centerPoint;

    // this is the knifes spawn poosition
    Vector3 spawnPosition;

    // this is the knifes spawn rotation.
    Quaternion spawnRotation;


    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        // get the center position using our center transform
        // we do this instead of just using the transform.position
        // to not always have it update and change depending on where 
        // the players head is at.
        centerPoint = centerTransform.position;
        
        // set the spawn position to be our starting position.
        spawnPosition = transform.position;

        // set the spawn rotation to be our starting rotation.
        spawnRotation = transform.rotation;
    }

    void Update()
    {
        // check if our discance between our center point (centerPoint) and current position (transform.position) 
        // is greater than our reach (playerReach)
        if(Vector3.Distance(centerPoint, transform.position) > playerReach)
		{
            // Respawn the knife.
			RespawnKnife();
		}
	}

	private void RespawnKnife()
	{
		// set our rigidbody's velocity to zero 
		// this makes sure that when the knife respawn it won't move
		body.velocity = Vector3.zero;

		// set our rotation to that of the spawn rotation we have
		transform.rotation = spawnRotation;

		// set our position to that of the spawn position we have 
		transform.position = spawnPosition;
	}

	// this is to draw a wireSphere and see the range before knife respawns.
    // This method is seen only in the editor and scene window
    // will run in the editor so unless we set centerTransform we get errors 
    // saying its null.
	private void OnDrawGizmos()
	{
        // sets the color of our sphere 
        Gizmos.color = Color.blue;
        
        // draws a sphere that is with wires (instead of DrawSphere that draws a complete sphere)
        Gizmos.DrawWireSphere(centerTransform.position, playerReach);
	}
}
