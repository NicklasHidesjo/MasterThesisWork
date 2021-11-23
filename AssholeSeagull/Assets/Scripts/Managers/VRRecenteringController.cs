using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRRecenteringController : MonoBehaviour
{
    /// <summary>
    /// Todo 
    /// fix so that it won't call in fixed update
    /// voice recognition?
    /// </summary>

    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Action_Boolean recenter = SteamVR_Input.GetBooleanAction("Recenter");
    private SteamVR_Action_Boolean snapUp = SteamVR_Input.GetBooleanAction("SnapUp");
    private SteamVR_Action_Boolean snapDown = SteamVR_Input.GetBooleanAction("SnapDown");

    [Tooltip("Desired head position of player when seated")]
    [SerializeField] private Transform desiredHeadPosition;
    [SerializeField] private Transform cameraRig;
    [SerializeField] private Transform steamCamera;


    [Tooltip("The maximum Y change a player can adjust their position with (both up and down)")]
    [SerializeField] private float yMaxChange;
    [Tooltip("The amount each up or down press will change our Y position")]
    [SerializeField] private float ySnap;

    float highestY;
    float lowestY;

	private void Start()
	{
        if (pose == null)
            pose = GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

        highestY = desiredHeadPosition.position.y + yMaxChange;
        lowestY = desiredHeadPosition.position.y - yMaxChange;
    }

    private void Update()
    {
        if(recenter.GetStateDown(pose.inputSource))
		{
            Recenter();
		}

        if(snapUp.GetStateDown(pose.inputSource))
		{
            Vector3 position = cameraRig.position;
            position.y += ySnap;
            position.y = Mathf.Clamp(position.y, lowestY, highestY);
            cameraRig.position = position;
		}
        if(snapDown.GetStateDown(pose.inputSource))
		{
            Vector3 position = cameraRig.position;
            position.y -= ySnap;
            position.y = Mathf.Clamp(position.y, lowestY, highestY);
            cameraRig.position = position;
        }


        // check so that our steamCamera has moved
        if(steamCamera.position.y > 0)
        {
            // check so that our cameraRig hasn't been changed yet
            if(cameraRig.position.y == 0)
            {
                // Recenter the camera to where the player is.
                Recenter();
            }
        }
    }

    public void Recenter()
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
            if(SceneLoader.GetSceneName() == "NewEndScene")
            {
                Vector3 rotation = desiredHeadPos.rotation.eulerAngles;
                rotation.y -= offsetAngle;

                cameraRig.rotation = Quaternion.Euler(rotation);
            }
        }
        else
        {
            Debug.Log("Error: SteamVR objects not found!");
        }
    }
}

