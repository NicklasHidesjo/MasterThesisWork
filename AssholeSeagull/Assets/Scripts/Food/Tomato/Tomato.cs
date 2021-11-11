using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Tomato : MonoBehaviour
{
    /// <TODO>
    /// Fix so that we drop it from the hand if we are attached to a hand
    /// </summary>
    [SerializeField] GameObject poop;

    [SerializeField] private int amountOfSlices = 5;
	[SerializeField] private float maxDistance = 0.5f;
    [SerializeField] float Spawnoffset;

    private int amountCut;
    private bool startedSlicing;
    private Vector3 spawnPos;

    private Rigidbody rb;
    private TomatoSpawner tomatoSpawner;
    private Interactable interactable;

    private AudioSource audioSource;
    private GameObject blade;

    private bool shitOn;
    public bool ShitOn
	{
		get
		{
            return shitOn;
		}
		set
		{
            shitOn = value;

            if(value)
			{
                poop.SetActive(true);
                audioSource.Play();
			}
		}
	}
    
	void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        poop.SetActive(false);
        shitOn = false;

        rb.velocity = Vector3.zero;
        KinematicToggle(true);

        amountCut = 0;
    }

    private void KinematicToggle(bool isKinematic)
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
            SpawnTomatoSlice();
            amountCut++;

            if (amountCut >= amountOfSlices)
            {
                DeactivateTomato();
            }
            startedSlicing = false;
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

        tomatoSlice.transform.position = spawnPos;
        tomatoSlice.KinematicToggle(false);
        tomatoSlice.gameObject.SetActive(true);
        tomatoSlice.PoopOnFood = shitOn;
    }

	public void DeactivateTomato()
    {
        if(interactable.attachedToHand)
		{
            interactable.attachedToHand.DetachObject(gameObject);
		}

        tomatoSpawner.SpawnNewTomato(this);
        gameObject.SetActive(false);
    }
}
