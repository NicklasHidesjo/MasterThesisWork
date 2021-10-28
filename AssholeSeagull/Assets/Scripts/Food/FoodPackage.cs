using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FoodPackage : MonoBehaviour
{
    // make sure that we can't add a sallad to the ham package for instance.


    [Header("Food Settings")]
    // the item that we will spawn as food
    [SerializeField] private FoodItem foodItem;
    // the name that that item will get
    [SerializeField] private string foodName;
    // the position that we want to spawn the food on.
    [SerializeField] private Transform spawnPosition;

    [Header("Poop")]
    // the poop that is on the object
    [SerializeField] private GameObject poop;
    [SerializeField] AudioClip poopOnFoodSound;

    // a list of all the food in the container
    private List<FoodItem> foodInPackage = new List<FoodItem>();

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
            // set wether shitInPackage should be true or false.
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

                FindObjectOfType<AudioPlayer>().SeagullFx(poopOnFoodSound);
            }
        }
    }
	
    private void Start()
	{
        // make sure that we spawn a food item in the begining (no empty packages)
        SpawnFoodItem();
        SetShitInPackage();
    }

	private void SpawnFoodItem()
	{
		// create a new FoodItem and save it as a local variable.
		FoodItem newFoodItem = Instantiate(foodItem, spawnPosition.position, foodItem.transform.rotation);

		newFoodItem.Init(foodName, shitInPackage);

		// add the newly created item to our foodInPackage list.
		AddFoodToContainer(newFoodItem);
	}
	private void SetShitInPackage()
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
        // go trough all the fooditems that lies in the package.
		foreach (var food in foodInPackage)
		{
            // let the food know that it has poop on it and should behave as such
            food.PoopOnFood = true;
		}
	}
	
    public void AddFoodToContainer(FoodItem food)
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
    public void RemoveFoodFromContainer(FoodItem food)
	{
        // set that the FoodItem(food) is not in the package.
        food.InPackage = false;

        // remove the FoodItem(food) from the list of food in the package.
        foodInPackage.Remove(food);

        // remove all of the elements that are null in foodInPackage,
        // so that we do not get a wrongfull count
        // a null element will still count as a element in the list,
        // thus increasing the count
        foodInPackage.RemoveAll(food => food == null);

        // check if the count in our package is lower than 1 (meaning the list is empty)
        if(foodInPackage.Count < 1) // make the one into a [serializeField] so that we can change it ourselves.
		{
            // spawn a new FoodItem as the list is empty and we need atleast one.
            SpawnFoodItem();
            SetShitInPackage();
		}
    }
}
