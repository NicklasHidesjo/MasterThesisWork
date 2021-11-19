using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public delegate void PickedUp();
public delegate void PutDown();
public class LettuceHead : MonoBehaviour
{
	public event PickedUp PickedUp;
    public event PutDown PutDown;

	[SerializeField] GameObject poop;

	private Interactable interactable;

	private Rigidbody rb;
	private LettuceSpawner lettuceSpawner;

	private AudioSource audioSource;

	List<Hand> attachedHands = new List<Hand>();

	private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
        lettuceSpawner = FindObjectOfType<LettuceSpawner>();
        rb = GetComponent<Rigidbody>();

        SubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        interactable.onAttachedToHand += TurnOffKinematic;

        interactable.onAttachedToHand += HandlePickedUp;
        interactable.onDetachedFromHand += HandlePutDown;
    }

    private void HandlePutDown(Hand hand)
    {
		attachedHands.Remove(hand);

		attachedHands.RemoveAll(hand => hand == null);

		if(attachedHands.Count == 0)
        {
			PutDown?.Invoke();
		}
    }

    private void HandlePickedUp(Hand hand)
    {
		attachedHands.Add(hand);
		if(attachedHands.Count == 1)
        {
			PickedUp?.Invoke();
		}
    }

    private void TurnOffKinematic(Hand hand)
    {
        if(rb.isKinematic)
        {
			rb.isKinematic = false;
        }
    }

	public void Deactivate()
    {
        poop.SetActive(false);
        lettuceSpawner.SpawnNewHead(this);
        rb.isKinematic = true;
        gameObject.SetActive(false);

        UnsubscribeFromEvents();
    }

    private void UnsubscribeFromEvents()
    {
        interactable.onAttachedToHand += TurnOffKinematic;
        interactable.onAttachedToHand -= HandlePickedUp;
        interactable.onDetachedFromHand -= HandlePutDown;
    }

	public void GotShitOn()
	{
		audioSource.Play();

		poop.SetActive(true);

		LeafSpawnPoint[] leafSpawnPoints = GetComponentsInChildren<LeafSpawnPoint>();
		foreach (var leaf in leafSpawnPoints)
		{
			leaf.SmearShitOnLeaf();
		}
	}
}
