using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    [SerializeField] private int amountOfSlices = 5;

    private int amountCut;
    private bool startedSlicing;
    private FoodPackage foodPackage;

    private void OnEnable()
    {
        amountCut = 0;
    }

    void Start()
    {
        //foodPackage = GetComponent<FoodPackage>();
    }

    public void SlicingTomato(bool finished)
    {
        if (!finished)
        {
            startedSlicing = true;
        }
        else if (finished && startedSlicing)
        {
            //foodPackage.ManuallySpawnFood();
            Debug.Log("Spawning tomato slice");
            amountCut++;

            if (amountCut >= amountOfSlices)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KnifeBlade"))
        {
            startedSlicing = false;
        }
    }

}
