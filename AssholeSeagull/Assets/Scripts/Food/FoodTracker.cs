using System.Collections.Generic;
using UnityEngine;

public class FoodTracker : MonoBehaviour
{
    private List<FoodItem> foodToSteal = new List<FoodItem>();

	private void Awake()
	{
		SubscribeToEvents();
	}
	private void SubscribeToEvents()
	{
		FoodItem.AddFood += AddFoodItem;
		FoodItem.RemoveFood += RemoveFoodItem;
	}

    private void AddFoodItem(FoodItem food)
    {
        // adds a transform to the list.
        foodToSteal.Add(food);
    }
    private void RemoveFoodItem(FoodItem food)
    {
        // removes a transform from the list.
        foodToSteal.Remove(food);
        // clear the list of nulls.
        foodToSteal.RemoveAll(Transform => transform == null);
    }

	private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	private void UnsubscribeFromEvents()
	{
		FoodItem.AddFood -= AddFoodItem;
		FoodItem.RemoveFood -= RemoveFoodItem;
	}
	 
    public FoodItem GetRandomTarget()
    {
        // check if we don't have any transforms in our list 
        if (foodToSteal.Count < 1)
		{
            // return null if we don't have any transforms in our list.
            return null;
		}

        // get a random integer based on the foodTransforms list size.
        int random = Random.Range(0, foodToSteal.Count);
        // get the transform of the foodTransforms element at position random.
        FoodItem foodItem = foodToSteal[random];

        RemoveFoodItem(foodItem);
        // return the transform.
        return foodItem;
    }
}
