using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRTrackingController : MonoBehaviour
{
 
    [Tooltip("Desired head position of player when seated")]
    [SerializeField] private Transform desiredHeadPosition;

    [SerializeField] private Transform steamCamera;
    [SerializeField] private Transform cameraRig;

	private void Update()
    {
        // check so that our steamCamera has moved
        if(steamCamera.transform.position.y > 0)
        {
            // check so that our cameraRig hasn't been changed yet
            if(cameraRig.transform.position.y == 0)
            {
                // Recenter the camera to where the player is.
                Recenter();
            }
        }
    }

    private void Recenter()
    {
        // check so that we have a desired position for our head.
        if (desiredHeadPosition != null)
        {
            // Change the actuall position and recenter it
            ResetSeatedPos(desiredHeadPosition);
        }
        else
        {
            Debug.LogError("Target Transform required. Assign in inspector.", gameObject);
        }
    }

    private void ResetSeatedPos(Transform desiredHeadPos)
    {
        if ((steamCamera != null) && (cameraRig != null))
        {
            //ROTATION
            // Get current head heading in scene (y-only, to avoid tilting the floor)
            float offsetAngle = steamCamera.rotation.eulerAngles.y;

            // Now rotate CameraRig in opposite direction to compensate
            cameraRig.Rotate(0f, -offsetAngle, 0f);

            //POSITION
            // Calculate postional offset between CameraRig and Camera
            Vector3 offsetPos = steamCamera.position - cameraRig.position;

            // Reposition CameraRig to desired position minus offset
            cameraRig.position = (desiredHeadPos.position - offsetPos);
        }
        else
        {
            Debug.Log("Error: SteamVR objects not found!");
        }
    }
}

