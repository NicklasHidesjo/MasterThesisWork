using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLayerTracker : MonoBehaviour
{
	// the distance that a ray gets cast to see what is above and below it.
	[SerializeField] private float rayDistance;

	[SerializeField] private LayerMask foodLayer;
	[SerializeField] private LayerMask onPlateLayer;

	FoodItem food;
	Rigidbody rb;

	void Start()
	{
		food = GetComponent<FoodItem>();
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (rb.IsSleeping())
		{
			return;
		}
		SetOnPlate();
	}

	private void SetOnPlate()
	{
		Vector3 down = new Vector3(0, -rayDistance, 0);
		RaycastHit hit;

		if (Physics.Linecast(transform.position, transform.position + down, out hit, onPlateLayer))
		{
			FoodItem foodHit = hit.collider.gameObject.GetComponent<FoodItem>();

			if (foodHit != null)
			{
				if (foodHit.OnPlate)
				{
					food.OnPlate = true;
					return;
				}
			}

			if (food.FoodType != FoodTypes.Bread)
			{
				food.OnPlate = false;
				return;
			}
			if (!food.Buttered)
			{
				food.OnPlate = false;
				return;
			}

			if (Physics.Linecast(transform.position, transform.position + down, out hit, onPlateLayer))
			{
				if (hit.transform.GetComponentInParent<Plate>())
				{
					food.OnPlate = true;
				}
			}
		}
		else
		{
			food.OnPlate = false;
		}
	}

	public void SetFoodAboveAndBelow()
	{
		// create a null RaycastHit object.
		RaycastHit hit;

		// create our 2 directions
		Vector3 up = new Vector3(0, rayDistance, 0);
		Vector3 down = new Vector3(0, -rayDistance, 0);

		DrawDebugRays(up, down);

		// cast the ray in one direction, and see if we hit something
		if (Physics.Linecast(transform.position, transform.position + up, out hit, foodLayer))
		{
			// get the FoodItem we hit
			FoodItem foodHit = hit.collider.gameObject.GetComponent<FoodItem>();

			// check that it's not null
			if (foodHit == null)
			{
				NullFoodAbove();
			}
			else
			{
				SetAboveFood(foodHit);
			}
		}
		else
		{
			NullFoodAbove();
		}

		// cast the ray in the other direction and do the same as for the above code.
		if (Physics.Linecast(transform.position, transform.position + down, out hit, foodLayer))
		{
			FoodItem foodHit = hit.collider.gameObject.GetComponent<FoodItem>();

			if (foodHit == null)
			{
				NullFoodBelow();
			}
			else
			{
				SetBelowFood(foodHit);
			}
		}
		else
		{
			NullFoodBelow();
		}
	}

	private void DrawDebugRays(Vector3 northSide, Vector3 southSide)
	{
		// debug draw those rays so we see them (remove this?)
		Debug.DrawRay(transform.position, northSide, Color.blue);
		Debug.DrawRay(transform.position, southSide, Color.red);
	}

	private void SetAboveFood(FoodItem foodHit)
	{
		// set foodAbove to be that of the FoodItem's(food) foodtype
		food.FoodAbove = foodHit.FoodType;
		// set the foodAboveAbove to be that of the FoodItem's(food) FoodAbove.
		food.FoodAboveAbove = foodHit.FoodAbove;
	}

	private void SetBelowFood(FoodItem foodHit)
	{
		food.FoodBelow = foodHit.FoodType;
		food.FoodBelowBelow = foodHit.FoodBelow;
	}

	private void NullFoodAbove()
	{
		// set both foodAbove and foodAboveAbove to none.
		food.FoodAbove = FoodTypes.None;
		food.FoodAboveAbove = FoodTypes.None;
	}

	private void NullFoodBelow()
	{
		// set both foodBelow and foodBelowBelow to none.
		food.FoodBelow = FoodTypes.None;
		food.FoodBelowBelow = FoodTypes.None;
	}
}
