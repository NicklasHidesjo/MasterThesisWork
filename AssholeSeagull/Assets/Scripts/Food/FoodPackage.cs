using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FoodPackage : MonoBehaviour
{
    // the item that we will spawn as food
    [SerializeField] FoodItem foodItem;

    // the name that that item will get
    [SerializeField] string foodName;

    // the poop that is on the object
    [SerializeField] GameObject poop;

    // the position that we want to spawn the poop on.
    [SerializeField] Transform spawnPosition;

    // a list of all the food in the container
    List<FoodItem> foodInContainer = new List<FoodItem>();

    // a bool to check if there is shit on the package or not
    bool shitOnPackage; // rename this? (if so make sure to check all comments using the variable name)
    // the amount of food items that will be spoiled when spawning 
    int spoiledFoods = 0;

	private void Start()
	{
        // make sure that we spawn a food item in the begining (no empty boxes)
        SpawnFoodItem();
	}

    // this needs to be above the start method
    // as it's a property and should be above methods below variables
	public bool ShitOnPackage // rename this? (if so make sure to check all comments using the variable name)
    {
        get { return shitOnPackage; }
        set
        {
            // set wether shitOnPackage should be true or false.
            shitOnPackage = value;
            // toggles the display of shit on package on/off
            poop.SetActive(value);
            
            // check if value = true
            if (value)
            {
                // increase the number of spoiled foods that will spawn.
                spoiledFoods ++;
                // spoil any food that is spawned lying in the package.
                PoopOnFoodInContainer(); // rename to a better name
            }
        }
    }

    public void PoopOnFoodInContainer()
	{
        // go trough all the fooditems that lies in the container.
		foreach (var food in foodInContainer)
		{
            // let the food know that it has poop on it and should behave as such
            food.PoopOnFood = true;
		}
	}

    private void SpawnFoodItem()
    {
        // create a new FoodItem and save it as a local variable.
        FoodItem newFoodItem = Instantiate(foodItem, spawnPosition.position, foodItem.transform.rotation);

        // set it's name
        newFoodItem.name = foodName; // do we need this?

        // set wether or not we have poop on the food based on if we have shit in the package
        newFoodItem.PoopOnFood = shitOnPackage;

        // set that the newly spawned item is in the package
        newFoodItem.InPackage = true;

        // reduce the number of spoiled food in the package (as we have spawned one)
        spoiledFoods--;
        // make sure that the spoiledFoods number won't go below 0
        spoiledFoods = (int) Mathf.Clamp(spoiledFoods, 0, Mathf.Infinity);

        // set shitOnPackage to be true if spoiledFoods is higher/more than 0
        shitOnPackage = spoiledFoods > 0;

        // set wether or not the poop model should be active or not using shitOnPackage
        poop.SetActive(shitOnPackage);

        // add our newly spawned FoodItem to our list of FoodItem's in the container.
        foodInContainer.Add(newFoodItem);
    }


    public void AddFoodToContainer(FoodItem food)
	{
        // let the food being added know that it's in the package
        food.InPackage = true;
        
        // a local bool to use if we have a duplicate food,
        // that should not be added to the container list.
        bool duplicate = false;
        // loop through each fooditem in the foodInContainer list
		foreach (var foodItem in foodInContainer)
		{
            // check if FoodItem(food) is the same as FoodItem(foodItem)
            if(foodItem == food)
			{
                // set duplicate to true if they are the same.
                duplicate = true;
			}
		}
        // check if duplicate is true.
        if(duplicate)
		{
            // return/exit this method and don't do the lines of code following after.
            return;
		}

        // if we don't have a duplicate we will get here and add the FoodItem(food),
        // to our list of food in the container.
        foodInContainer.Add(food);
	}

    public void RemoveFoodFromContainer(FoodItem food)
	{
        // set that the FoodItem(food) is not in the container.
        food.InPackage = false;

        // remove the FoodItem(food) from the list of food in the container.
        foodInContainer.Remove(food);

        // remove all of the elements that are null in foodInContainer,
        // so that we do not get a wrongfull count
        // a null element will still count as a element in the list,
        // thus increasing the count
        foodInContainer.RemoveAll(food => food == null);

        // check if the count in our container is lower than 1 (meaning the list is empty)
        if(foodInContainer.Count < 1) // make the one into a [serializeField] so that we can change it ourselves.
		{
            // spawn a new FoodItem as the list is empty and we need atleast one.
            SpawnFoodItem();
		}
    }
}
