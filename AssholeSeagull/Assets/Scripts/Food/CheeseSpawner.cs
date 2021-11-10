using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseSpawner : MonoBehaviour
{

    private bool startedSlicing;
    private FoodPackage foodPackage;

    void Start()
    {
        foodPackage = GetComponent<FoodPackage>();
    }

    public void SlicingCheese(bool finished)
    {
        if (!finished)
        {
            startedSlicing = true;
        }
        else if (finished && startedSlicing) 
        {
            foodPackage.ManuallySpawnFood();
            Debug.Log("Spawning cheese");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheeseSlicer"))
        {
            startedSlicing = false;
        }
    }
}
