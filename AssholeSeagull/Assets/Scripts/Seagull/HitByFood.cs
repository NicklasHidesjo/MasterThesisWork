using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByFood : MonoBehaviour
{
    private SeagullController seagullController;

    float velocityLimit; 
    void Start()
    {
        seagullController = GetComponentInParent<SeagullController>();
        velocityLimit = seagullController.SeagullSettings.velocityLimit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") || other.CompareTag("Head") || other.CompareTag("Tomato"))
        {
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            if(otherRB == null)
            {
                otherRB = other.GetComponentInChildren<Rigidbody>();
            }
            if(otherRB == null)
            {
                return;
            }    

            float othersVelocity = Mathf.Abs(otherRB.velocity.magnitude);
            if(othersVelocity >= velocityLimit)
            {
                seagullController.IsScared = true;
            }
        }
    }
}
