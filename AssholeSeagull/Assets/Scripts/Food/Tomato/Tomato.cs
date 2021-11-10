using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    [SerializeField] private int amountOfSlices = 5;

    private int amountCut;
    private bool startedSlicing;
    private FoodPackage foodPackage;
    private Rigidbody rb;
    private TomatoSpawner tomatoSpawner;

    void Awake()
    {
        //foodPackage = GetComponent<FoodPackage>();
        rb = GetComponent<Rigidbody>();
        tomatoSpawner = FindObjectOfType<TomatoSpawner>();
    }
    private void OnEnable()
    {
        amountCut = 0;
    }

    public void KinematicToggle(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
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
                DeactivateTomato();
            }
        }
    }

    public void DeactivateTomato()
    {
        tomatoSpawner.SpawnNewTomato(this);
        gameObject.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("KnifeBlade"))
        {
            startedSlicing = false;
        }
    }
}
