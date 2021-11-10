using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoSpawner : MonoBehaviour
{
	[SerializeField] private Tomato tomato;
	[SerializeField] private int tomatoPoolSize = 5;
	[SerializeField] private float maxDistance = 1f;
	
	[SerializeField] private FoodItem tomatoSlice;
	[SerializeField] private int tomatoSlicePoolSize;

	[SerializeField] private Transform tomatoParent;

	private List<Tomato> tomatoPool = new List<Tomato>();
	private List<FoodItem> tomatoSlicePool = new List<FoodItem>();

	[SerializeField] private Tomato activeTomato;

	void Start()
	{
		CreateTomatoPool();
		CreateTomatoSlicePool();

		HandleSpawnTomato();
	}

	private void FixedUpdate()
	{
		if(Vector3.Distance(activeTomato.transform.position, transform.position) >= maxDistance)
		{
			HandleSpawnTomato();
		}
	}

	private void CreateTomatoSlicePool()
	{
		for (int i = 0; i < tomatoSlicePoolSize; i++)
		{
			FoodItem tmp = Instantiate(tomatoSlice, tomatoParent);
			tmp.gameObject.SetActive(false);
			tmp.Init("Tomato Slice");
			tomatoSlicePool.Add(tmp);
		}
	}

	private void CreateTomatoPool()
	{
		Tomato tmp;
		for (int i = 0; i < tomatoPoolSize; i++)
		{
			tmp = Instantiate(tomato, tomatoParent);
			tmp.gameObject.SetActive(false);
			tomatoPool.Add(tmp);
		}
	}

	public void SpawnNewTomato(Tomato tomato)
	{
		if (tomato == activeTomato)
		{
			HandleSpawnTomato();
		}
	}

	private void HandleSpawnTomato()
	{
		foreach (var tomato in tomatoPool)
		{
			if (tomato.gameObject.activeSelf)
			{
				continue;
			}
			else
			{
				SpawnTomato(tomato);
				return;
			}
		}
		Tomato tmp = Instantiate(tomato);
		SpawnTomato(tmp);
		tomatoPool.Add(tmp);
	}

	private void SpawnTomato(Tomato tomato)
	{
		activeTomato = tomato;
		tomato.transform.position = transform.position;
		tomato.transform.rotation = transform.rotation;
		tomato.KinematicToggle(true);
		tomato.gameObject.SetActive(true);
	}

	public FoodItem GetTomatoSlice()
	{
		FoodItem newTomatoSlice = null;
		foreach (var slice in tomatoSlicePool)
		{
			if(slice.gameObject.activeSelf)
			{
				continue;
			}
			newTomatoSlice = slice;
			break;
		}
		if(newTomatoSlice == null)
		{
			newTomatoSlice = Instantiate(tomatoSlice);
			newTomatoSlice.Init("Tomato Slice");
		}
		return newTomatoSlice;
	}
}
