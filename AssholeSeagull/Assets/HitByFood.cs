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
        if (other.CompareTag("Food") || other.CompareTag("Head") || other.CompareTag("Tomato"))
        {
            seagullController.IsScared = true;
        }
    }
}
