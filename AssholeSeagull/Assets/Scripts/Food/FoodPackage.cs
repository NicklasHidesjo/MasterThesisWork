using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FoodPackage : MonoBehaviour
{
	/// <todo>
	/// object pool for food items
	/// </summary>
	[Header("Food Settings")]
	// the item that we will spawn as food
	[SerializeField] private FoodItem foodItem;
	[SerializeField] private Transform foodParent;
	// the name that that item will get
	[SerializeField] private string foodName;
	// the position that we want to spawn the food on.
	[SerializeField] private Transform spawnPosition;
	[SerializeField] private int foodPoolSize = 10;
	[SerializeField] private bool automaticSpawning = true;

	[Header("Poop")]
	// the poop that is on the object
	[SerializeField] private GameObject poop;

	private List<FoodItem> foodItemPool = new List<FoodItem>();
	// a list of all the food in the container
	private List<FoodItem> foodInPackage = new List<FoodItem>();

	private AudioSource audioSource;

	// the amount of food items that will be spoiled when spawning 
	private int poopedFoods = 0;

	// a bool to check if there is shit on the package or not
	private bool shitInPackage = false;
	public bool ShitInPackage
	{
		get 
		{ 
			return shitInPackage; 
		}
		set
		{
			// set whether shitInPackage should be true or false.
			shitInPackage = value;
			// toggles the display of shit on package on/off
			poop.SetActive(value);
			
			// check if value = true
			if (value)
			{
				// increase the number of foods with poop on it that will spawn.
				poopedFoods ++;
				// smear poop on any food that is spawned lying in the package.
				SmearPoopInPackage();

				audioSource.Play();
			}
		}
	}
	
	private void Start()
	{
		CreateItemPool();
		SetShitInPackage();

		if (automaticSpawning)
		{
			 SpawnFoodItem();
		}

		audioSource = GetComponent<AudioSource>();
	}

	private void CreateItemPool()
	{
		FoodItem tmp;
		for (int i = 0; i < foodPoolSize; i++)
		{
			tmp = Instantiate(foodItem,foodParent);
			tmp.gameObject.SetActive(false);
			tmp.Init(foodName);
			foodItemPool.Add(tmp);
		}
	}

	private void SpawnFoodItem()
	{
		FoodItem newFoodItem = GetFoodFromPool();

		if (newFoodItem == null)
		{
			newFoodItem = Instantiate(foodItem, foodParent);
			newFoodItem.Init(foodName);
			foodItemPool.Add(newFoodItem);
		}
		
		newFoodItem.transform.position = spawnPosition.position;
		newFoodItem.transform.rotation = foodItem.transform.rotation * transform.rotation;

		newFoodItem.KinematicToggle(true);
		newFoodItem.gameObject.SetActive(true);
		
		newFoodItem.PoopOnFood = shitInPackage;

		// add the newly created item to our foodInPackage list.
		AddFoodToContainer(newFoodItem);
	}

	private FoodItem GetFoodFromPool()
	{
		foreach (var item in foodItemPool)
		{
			if (item.gameObject.activeSelf)
			{
				continue;
			}
			return item;
		}
		return null;
	}

	private void SetShitInPackage() //rename
	{
		// reduce the number of food with poop on it, in the package (as we have spawned one)
		poopedFoods--;
		// make sure that the poopedFoods number won't go below 0
		poopedFoods = (int)Mathf.Clamp(poopedFoods, 0, Mathf.Infinity);

		// set shitInPackage to be true if poopedFoods is higher/more than 0
		shitInPackage = poopedFoods > 0;

		// set wether or not the poop model should be active or not using shitInPackage
		poop.SetActive(shitInPackage);
	}

	private void SmearPoopInPackage()
	{
		// go through all the fooditems that lie in the package.
		foreach (var food in foodInPackage)
		{
			// let the food know that it has poop on it and should behave as such
			food.PoopOnFood = true;
		}
	}
	
	// this will be removed probably (as we won't be able to add things to package anymore.
	// will be changed at the least.
	private void AddFoodToContainer(FoodItem food)
	{
		// let the food being added know that it's in the package
		food.InPackage = true;

		// loop through each fooditem in the foodInPackage list
		foreach (var foodItem in foodInPackage)
		{
			// check if FoodItem(food) is the same as FoodItem(foodItem)
			if(foodItem == food)
			{
				return;
			}
		}

		// add the FoodItem(food) to our list of food in the package.
		foodInPackage.Add(food);
	}

	public void ManuallySpawnFood()
    {
		SpawnFoodItem();
		SetShitInPackage();
	}

	public void RemoveFoodFromContainer(FoodItem food)
	{
		// set that the FoodItem(food) is not in the package.
		food.InPackage = false;

		#if UNITY_EDITOR
		food.KinematicToggle(false);
		#endif

		// remove the FoodItem(food) from the list of food in the package.
		foodInPackage.Remove(food);

		// remove all of the elements that are null in foodInPackage,
		// so that we do not get a wrongfull count
		// a null element will still count as a element in the list,
		// thus increasing the count
		foodInPackage.RemoveAll(food => food == null);

		// check if the count in our package is lower than 1 (meaning the list is empty)
		if(foodInPackage.Count < 1 && automaticSpawning) // make the one into a [serializeField] so that we can change it ourselves.
		{
			// spawn a new FoodItem as the list is empty and we need atleast one.
			SpawnFoodItem();
			SetShitInPackage();
		}
	}
}
