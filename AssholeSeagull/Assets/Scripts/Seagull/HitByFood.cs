using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByFood : MonoBehaviour
{
    private SeagullController seagullController;

    void Start()
    {
        seagullController = GetComponentInParent<SeagullController>();
    }

    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("I got hit by food" + other.gameObject.name);
            seagullController.IsScared = true;
    }
}
