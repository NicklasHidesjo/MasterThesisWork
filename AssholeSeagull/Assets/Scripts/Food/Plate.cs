using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [Tooltip("The maximum height that the sandwich can have (in unity units)")]
    [SerializeField] private float rayDistance;
    [Tooltip("The radius of detection of the ray from the center of the plate")]
    [SerializeField] private float rayRadius;
    [Tooltip("The layer that only food is on")]
    [SerializeField] private LayerMask foodLayer;

    // a list of everything that is on the plate/sandwich
    [SerializeField] private List<FoodItem> sandwichPieces = new List<FoodItem>(); // remove SerializeField only for debugging.
    public List<FoodItem> SandwichPieces 
    { 
        get 
        { 
            return sandwichPieces; 
        } 
    }

	private void OnEnable()
	{
        FoodItem.AddFoodToPlate += AddSandwichItem;
        FoodItem.RemoveFoodFromPlate += RemoveSandwichItem;
	}

	public void AddSandwichItem(FoodItem food)
	{
        sandwichPieces.Add(food);
		if(food.FoodType == FoodTypes.Bread)
		{
			if(!food.Buttered)
			{
				FindObjectOfType<ScoreManager>().FinishSandwich(true);
			}
		}
	}
    public void RemoveSandwichItem(FoodItem food)
	{
        sandwichPieces.Remove(food);
        sandwichPieces.RemoveAll(item => item == null);
	}

	private void OnDisable()
	{
		FoodItem.AddFoodToPlate -= AddSandwichItem;
		FoodItem.RemoveFoodFromPlate -= RemoveSandwichItem;
	}
}
