using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRTrackingController : MonoBehaviour
{
 
    [Tooltip("Desired head position of player when seated")]
    [SerializeField] Transform desiredHeadPosition;

    [SerializeField] Transform steamCamera;
    [SerializeField] Transform cameraRig;

	private void Update()
    {
        if(steamCamera.transform.position.y > 0)
        {
            if(cameraRig.transform.position.y == 0)
            {
                Debug.Log("Recentering");
                Recenter();
            }
        }
    }

    private void Recenter()
    {
        if (desiredHeadPosition != null)
        {
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

            Debug.Log("Seat recentered!");
        }
        else
        {
            Debug.Log("Error: SteamVR objects not found!");
        }
    }
}

