using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
	FoodPackage package;
	private void Start()
	{
		package = GetComponentInParent<FoodPackage>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Food"))
		{
			FoodItem food = other.GetComponent<FoodItem>();
			if(food != null)
			{
				package.AddFoodToContainer(food);
			}
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Food"))
		{
			FoodItem food = other.GetComponent<FoodItem>();
			if (food != null)
			{
				package.RemoveFoodFromContainer(food);
			}
		}
	}
}
