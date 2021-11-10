using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LettuceSpawner : MonoBehaviour
{
	[SerializeField] private FoodItem lettuceLeaf;
	[SerializeField] private int leafPoolSize = 3;

	[SerializeField] private LettuceHead lettuceHead;
	[SerializeField] private int headPoolSize = 2;

	[SerializeField] private Transform spawnPoint;
	[SerializeField] private Transform foodParent;

	private List<FoodItem> leafPool = new List<FoodItem>();
	private List<LettuceHead> headPool = new List<LettuceHead>();

	[SerializeField] private LettuceHead activeHead;

	private void Awake()
	{
		CreateLeafPool();
		CreateHeadPool();
	}

	private void OnEnable()
	{
		FoodItem.RemoveFromLettuceHead += CheckIfEmpty;
	}
	private void Start()
	{
		HandleSpawningHead();
	}

	private void CreateLeafPool()
	{
		FoodItem tmp;
		for (int i = 0; i < leafPoolSize; i++)
		{
			tmp = Instantiate(lettuceLeaf, foodParent);
			tmp.gameObject.SetActive(false);
			tmp.Init("lettuce");
			leafPool.Add(tmp);
		}
	}

	private void CheckIfEmpty()
	{
		LeafSpawnPoint[] leafSpawnPoints = activeHead.GetComponentsInChildren<LeafSpawnPoint>();

		foreach (var item in leafSpawnPoints)
		{
			if (item.HasChild())
			{
				return;
			}
		}
		HandleSpawningHead();
	}

	private void CreateHeadPool()
	{
		LettuceHead tmp;
		for (int i = 0; i < headPoolSize; i++)
		{
			tmp = Instantiate(lettuceHead);
			tmp.gameObject.SetActive(false);
			headPool.Add(tmp);
		}
	}

	private void HandleSpawningHead()
	{
		LettuceHead newHead = null;
		foreach (var head in headPool)
		{
			if (head.gameObject.activeSelf)
			{
				continue;
			}
			newHead = head;
			break;
		}

		if (newHead == null)
		{
			newHead = Instantiate(lettuceHead);
			headPool.Add(newHead);
		}

		SpawnLettuceHead(newHead);
	}

	private void SpawnLettuceHead(LettuceHead newHead)
	{
		newHead.transform.position = spawnPoint.position;
		newHead.transform.rotation = spawnPoint.rotation;
		newHead.gameObject.SetActive(true);
		activeHead = newHead;
	}

	public FoodItem GetLettuceLeaf()
	{
		FoodItem food = null;
		foreach (var leaf in leafPool)
		{
			if (leaf.gameObject.activeSelf)
			{
				continue;
			}
			food = leaf;
			break;
		}

		if (food == null)
		{
			food = Instantiate(lettuceLeaf);
			food.Init("lettuce");
			leafPool.Add(food);
		}
		return food;
	}

	public void SpawnNewHead(LettuceHead head)
	{
		if (activeHead == head)
		{
			HandleSpawningHead();
		}
	}

	private void OnDisable()
	{
		FoodItem.RemoveFromLettuceHead -= CheckIfEmpty;
	}
}
