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

	public bool Spoiled
    {
        get
        {
			return spoiled;
        }
        set
        {
			spoiled = value;
			timer = 0;
        }
    }


	private void OnEnable()
	{
		FoodItem.Reset += Reset;
	}

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
		if (food.InHand || body.isKinematic || food.OnPlate || food.Stolen)
		{
			// return/exit and don't run the code below
			return;
		}

		// check if we are moving
		if (!body.IsSleeping())
		{
			return;
		}

		IncreaseTimer();
		
		if (spoiled)
        {
            DeactivateFood();
        }
        else if(timer > foodSettings.spoilTime)
        {
            SpoilFood();
        }
    }
    private void DeactivateFood()
    {
        // check if our timer is larger then our selfDestructTime and that we are also not on the plate
        if (timer > foodSettings.DeactivateTime)
        {
			food.DeactivateFood();
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

	private void Reset(FoodItem food)
	{
		if(this.food == food)
		{
			Spoiled = false;
		}
	}

	private void OnDisable()
	{
		FoodItem.Reset -= Reset;
	}
}
