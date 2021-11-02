using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpoiledHandler : MonoBehaviour
{
	private FoodItem food;
	private Rigidbody body;
	private FoodSettings foodSettings;

	// the timer to check if food is spoiled or should be destroyed.
	private float timer = 0;

	private bool spoiled;

	private void Start()
	{
		if (GameManager.Settings.AlwaysFreshFood)
		{
			enabled = false;
		}

		food = GetComponent<FoodItem>();
		body = GetComponent<Rigidbody>();
		foodSettings = food.FoodSettings;
	}

	private void FixedUpdate()
	{
		// check if any of these bools are true
		if (food.InHand || food.InPackage || food.OnPlate || food.Stolen)
		{
			// return/exit and don't run the code below
			return;
		}

		// check if we are moving
		if (IsMoving())
		{
			food.Moving = true;
			// return/exit and don't run the code below
			return;
		}
		else
		{
			food.Moving = false;
		}

		IncreaseTimer();
		
		if (spoiled)
        {
            DestroyFood();
        }
        else if(timer > foodSettings.spoilTime)
        {
            SpoilFood();
        }
    }
    private void DestroyFood()
    {
        // check if our timer is larger then our selfDestructTime and that we are also not on the plate
        if (timer > foodSettings.selfDestructTime)
        {
            // destroy the foodItem
            // (this will be changed to deactivate as we will have a object pool on foodPackage)
            Destroy(gameObject);
        }
    }

    private void SpoilFood()
    {
        // set isSpoiled based on if our timer is larger then our spoilTime
        timer = 0;
        food.IsSpoiled = true;
        spoiled = true;
    }

    private void IncreaseTimer()
	{
		// increase our timer
		timer += Time.fixedDeltaTime;
	}

	private bool IsMoving()
	{
		// return true if our rigidbody's(body) velocity is larger then our velocityThreshold.
		return body.velocity.sqrMagnitude > foodSettings.velocityThreshold;
	}
}
