using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseTrigger : MonoBehaviour
{
    [SerializeField] private bool startPoint;
    private CheeseSpawner cheeseSpawner;


    private void Start()
    {
        cheeseSpawner = GetComponentInParent<CheeseSpawner>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheeseSlicer"))
        {
            if (startPoint)
            {
                cheeseSpawner.SlicingCheese(false);
            }
            else
            {
                cheeseSpawner.SlicingCheese(true);
            }
        }
    }
}
