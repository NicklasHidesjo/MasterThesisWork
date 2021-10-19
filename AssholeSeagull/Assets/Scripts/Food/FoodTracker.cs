using System.Collections.Generic;
using UnityEngine;

public class FoodTracker : MonoBehaviour
{
    private List<Transform> foodTransforms = new List<Transform>();

	private void Awake()
	{
		SubscribeToEvents();
	}
	private void SubscribeToEvents()
	{
		FoodItem.AddFood += AddFoodTransform;
		FoodItem.RemoveFood += RemoveFoodTransform;
	}

    private void AddFoodTransform(FoodItem food)
    {
        // adds a transform to the list.
        foodTransforms.Add(food.transform);
    }
    private void RemoveFoodTransform(FoodItem food)
    {
        // removes a transform from the list.
        foodTransforms.Remove(food.transform);
        // clear the list of nulls.
        foodTransforms.RemoveAll(Transform => transform == null);
    }

	private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	private void UnsubscribeFromEvents()
	{
		FoodItem.AddFood -= AddFoodTransform;
		FoodItem.RemoveFood -= RemoveFoodTransform;
	}
	 
    public Transform GetRandomTarget()
    {
        // check if we don't have any transforms in our list 
        if (foodTransforms.Count < 1)
		{
            // return null if we don't have any transforms in our list.
            return null;
		}

        // get a random integer based on the foodTransforms list size.
        int random = Random.Range(0, foodTransforms.Count);
        // get the transform of the foodTransforms element at position random.
        Transform foodTransform = foodTransforms[random];

        // return the transform.
        return foodTransform;
    }
}
