using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettuceSpawner : MonoBehaviour
{
	[SerializeField] private FoodItem lettuceLeaf;
	[SerializeField] private int leafPoolSize = 3;

	[SerializeField] private GameObject lettuceHead;
	[SerializeField] private int headPoolSize = 2;

	[SerializeField] private Transform spawnPoint;

	private List<FoodItem> leafPool = new List<FoodItem>();
	private List<GameObject> headPool = new List<GameObject>();

	private void Awake()
	{
		CreateLeafPool();
		CreateHeadPool();
	}
	private void Start()
	{
		SpawnLettuceHead();
	}

	private void CreateLeafPool()
	{
		FoodItem tmp;
		for (int i = 0; i < leafPoolSize; i++)
		{
			tmp = Instantiate(lettuceLeaf);
			tmp.gameObject.SetActive(false);
			tmp.Init("lettuce");
			leafPool.Add(tmp);
		}
	}

	private void CreateHeadPool()
	{
		GameObject tmp;
		for (int i = 0; i < headPoolSize; i++)
		{
			tmp = Instantiate(lettuceHead);
			tmp.SetActive(false);
			headPool.Add(tmp);
		}
	}

	private void SpawnLettuceHead()
	{
		foreach (var item in headPool)
		{
			if (item.activeSelf)
			{
				continue;
			}
			item.transform.position = spawnPoint.position;
			item.transform.rotation = spawnPoint.rotation;
			item.SetActive(true);
			return;
		}

		GameObject tmp;
		tmp = Instantiate(lettuceHead);
		tmp.transform.position = spawnPoint.position;
		tmp.transform.rotation = spawnPoint.rotation;
		tmp.SetActive(true);
		headPool.Add(tmp);
	}

	public FoodItem GetLettuceLeaf()
	{
		FoodItem tmp;
		foreach (var item in leafPool)
		{
			if (item.gameObject.activeSelf)
			{
				continue;
			}
			return item;
		}
		tmp = Instantiate(lettuceLeaf);
		tmp.Init("lettuce");
		leafPool.Add(tmp);
		return tmp;
	}
}
