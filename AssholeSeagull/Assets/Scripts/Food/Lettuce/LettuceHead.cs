using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LettuceHead : MonoBehaviour
{
	[SerializeField] GameObject poop;
	
	private Interactable interactable;
	private Rigidbody rb;
	private LettuceSpawner lettuceSpawner;

	private AudioSource audioSource;

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


	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		interactable = GetComponent<Interactable>();
		lettuceSpawner = FindObjectOfType<LettuceSpawner>();
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if(rb.isKinematic && interactable.attachedToHand)
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
	}
}
