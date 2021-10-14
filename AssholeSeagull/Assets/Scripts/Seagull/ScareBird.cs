using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareBird : MonoBehaviour
{
    public SeagullMovement seagullMovement = null;

    void Update()
    {
        if(seagullMovement != null)
        {
            if (Input.GetKeyDown(KeyCode.M) && seagullMovement.inDistance == true && seagullMovement.isPoopingTime == false)
            {
                Debug.Log("Scared bird");
                seagullMovement.Scared();
            }
        }
    }
}
