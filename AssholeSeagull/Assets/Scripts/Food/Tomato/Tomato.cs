using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Tomato : MonoBehaviour
{
    [SerializeField] GameObject poop;

    [SerializeField] private int amountOfSlices = 5;
	[SerializeField] private float maxDistance = 0.5f;
    [SerializeField] float Spawnoffset;

    private int amountCut;
    private bool startedSlicing;
    private Vector3 spawnPos;
    private Quaternion spawnRotation;

    private Rigidbody rb;
    private TomatoSpawner tomatoSpawner;
    private Interactable interactable;
    private ComplexThrowable complexThrowable;

    private AudioSource audioSource;
    private GameObject blade;

    private bool shitOn;
	private bool shouldDeactivate;

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
        complexThrowable = GetComponent<ComplexThrowable>();

        tomatoSpawner = FindObjectOfType<TomatoSpawner>();
    }
	private void OnEnable()
    {
        poop.SetActive(false);
        shitOn = false;
        rb.velocity = Vector3.zero;
        KinematicToggle(true);
        shouldDeactivate = false;
        amountCut = 0;
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

        // change this to use interactable onattached event
		if(rb.isKinematic && interactable.attachedToHand)
		{
            KinematicToggle(false);
        }
	}


    private void KinematicToggle(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
    }

    public void SlicingTomato(bool finished, GameObject blade)
    {
        if(shouldDeactivate)
		{
            return;
		}

        this.blade = blade;

        if (!startedSlicing && !finished)
		{
			startedSlicing = true;
        }
		else if (finished && startedSlicing)
        {
            SetSpawnPosition(blade);
            SpawnTomatoSlice();
            amountCut++;

            if (amountCut >= amountOfSlices)
            {
                ShouldDeactivateTomato();
            }
            startedSlicing = false;
        }
    }

	private void SetSpawnPosition(GameObject blade)
	{
        Vector2 bladePos = new Vector2(blade.transform.position.x, blade.transform.position.z);
        Vector2 tomatoPos = new Vector2(transform.position.x, transform.position.z);

        Vector2 rawDirection = (bladePos - tomatoPos).normalized;

        Vector3 direction = new Vector3(rawDirection.x, 0, rawDirection.y);

        spawnRotation = Quaternion.LookRotation(direction);

        spawnPos = transform.position + (direction * Spawnoffset);
    }

	private void SpawnTomatoSlice()
	{
        Debug.Log("Spawning tomato slice");

        FoodItem tomatoSlice = tomatoSpawner.GetTomatoSlice();

        tomatoSlice.transform.position = spawnPos;
        tomatoSlice.transform.rotation = spawnRotation;
        tomatoSlice.KinematicToggle(false);
        tomatoSlice.gameObject.SetActive(true);
        tomatoSlice.PoopOnFood = shitOn;
    }

	public void ShouldDeactivateTomato()
    {
        Hand hand = interactable.attachedToHand;
        complexThrowable.PhysicsDetach(hand);

        Invoke("DeactivateTomato", 0.5f);

        shouldDeactivate = true;
        tomatoSpawner.SpawnNewTomato(this);
    }
    private void DeactivateTomato()
	{
        gameObject.SetActive(false);
    }


	private void OnDrawGizmos()
	{
        Gizmos.color = new Color(0, 128, 128);
        Gizmos.DrawWireSphere(transform.position, Spawnoffset);
	}
}
