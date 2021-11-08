using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
	private FoodPackage package;

	private void Start()
	{
		GetReferences();
	}

	private void GetReferences()
	{
		// get a reference to the package that this object is a child to.
		package = GetComponentInParent<FoodPackage>();
	}

		#if UNITY_EDITOR
	private void OnTriggerExit(Collider other)
	{
		TryRemovingFoodFromContainer(other);
	}
		#endif

	private void TryRemovingFoodFromContainer(Collider other)
	{
		// check so that we have the correct tag.
		if (other.CompareTag("Food"))
		{
			// Get the FoodItem component of the object that exited our collider.
			FoodItem food = other.GetComponentInParent<FoodItem>();

			// check if the FoodItem(food) is not null 
			if (food != null) // make this into a if null return?
			{
				// start the process of removing the FoodItem(food) from the container.
				package.RemoveFoodFromContainer(food);
			}
		}

	}
}
