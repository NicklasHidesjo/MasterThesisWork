using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBird : MonoBehaviour
{
    // check if this script is being used as we now have WaveSeagull instead (remove if not in use)

    public SeagullMovement seagullMovement = null;

    void Update()
    {
        if(seagullMovement != null)
        {
            // checks if we should scare the bird 
            if (Input.GetKeyDown(KeyCode.M) && seagullMovement.inDistance == true && seagullMovement.isPoopingTime == false)
            {
                Debug.Log("Scared bird");
                seagullMovement.Scared();
            }
        }
    }
}
