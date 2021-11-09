using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LettuceHead : MonoBehaviour
{
	private Interactable interactable;
	private Rigidbody rb;
	private LettuceSpawner lettuceSpawner;


	private void Start()
	{
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
		lettuceSpawner.SpawnNewHead(this);
		rb.isKinematic = true;
		gameObject.SetActive(false);
	}
}
