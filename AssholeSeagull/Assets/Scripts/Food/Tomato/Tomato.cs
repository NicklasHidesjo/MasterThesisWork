using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Tomato : MonoBehaviour
{
    [SerializeField] private int amountOfSlices = 5;
	[SerializeField] private float maxDistance = 0.5f;
    [SerializeField] float Spawnoffset;

    private int amountCut;
    private bool startedSlicing;
    private Rigidbody rb;
    private TomatoSpawner tomatoSpawner;
    private Interactable interactable;

    private Vector3 spawnPos;

    private GameObject blade;
    
	void Awake()
    {
        rb = GetComponent<Rigidbody>();
        interactable = GetComponent<Interactable>();
        tomatoSpawner = FindObjectOfType<TomatoSpawner>();
        Spawnoffset = transform.localScale.x / 2;
    }

	private void FixedUpdate()
	{
        if(startedSlicing && blade != null)
		{
            if (Vector3.Distance(blade.transform.position, transform.position) >= maxDistance)
			{
                startedSlicing = false;
			}
		}

		if(rb.isKinematic && interactable.attachedToHand)
		{
            KinematicToggle(false);
        }
	}

	private void OnEnable()
    {
        amountCut = 0;
    }

    public void KinematicToggle(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
    }

    public void SlicingTomato(bool finished, GameObject blade)
    {
        this.blade = blade;

        if (!startedSlicing && !finished)
		{
			startedSlicing = true;
            SetSpawnPosition(blade);
        }
		else if (finished && startedSlicing)
        {
            Debug.Log("Spawning tomato slice");
            SpawnTomatoSlice();
            amountCut++;

            if (amountCut >= amountOfSlices)
            {
                DeactivateTomato();
            }
        }
    }

	private void SetSpawnPosition(GameObject blade)
	{
        // fix so that it spawns related to where the knife cuts.
        spawnPos = transform.position;
    }

	private void SpawnTomatoSlice()
	{
        Debug.Log("Spawning tomato slice");

        FoodItem tomatoSlice = tomatoSpawner.GetTomatoSlice();

        Debug.Log(tomatoSlice);

        tomatoSlice.transform.position = spawnPos;
        tomatoSlice.KinematicToggle(false);
        tomatoSlice.gameObject.SetActive(true);

        // get the direction that the knife is in from the center point 
        // spawn a tomatoSlice with an offset in that direction
        // rotate the slice appropriately
        startedSlicing = false;
    }

	public void DeactivateTomato()
    {
        tomatoSpawner.SpawnNewTomato(this);
        gameObject.SetActive(false);
    }
}
