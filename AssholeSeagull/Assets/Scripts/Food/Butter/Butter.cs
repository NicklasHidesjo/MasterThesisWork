using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butter : MonoBehaviour
{
	private float butteredPercentage;

	[Tooltip("The threshold for each butter-stage to be initiated at.")]
	[SerializeField] private List<float> butterStageInitiation;

	[Tooltip("The objects to be toggled on/off based on the buttered amount")]
	[SerializeField] private GameObject[] butterObjects;

	private KnifeVelocity knife;
	private bool buttered;

	private void OnEnable()
	{
		FoodItem.Reset += Reset;
	}

	private void FixedUpdate()
	{
		if(!buttered)
		{
			return;
		}
		if (knife == null) 
		{ 
			return; 
		}
		

		// increase the amount that is buttered.
		butteredPercentage += knife.Velocity;

		ChangeButterStage();
	}

	private void ChangeButterStage()
	{
		if (butteredPercentage > butterStageInitiation[1] && !butterObjects[2].activeSelf)
		{
			DeactivateAll();
			ActivateButterStage(butterObjects[2]);
		}
		else if (butteredPercentage > butterStageInitiation[0] && !butterObjects[2].activeSelf)
		{
			DeactivateAll();
			ActivateButterStage(butterObjects[1]);
		}
	}

	private void DeactivateAll()
	{
		butterObjects[0].SetActive(false);
		butterObjects[1].SetActive(false);
		butterObjects[2].SetActive(false);
	}
	private void ActivateButterStage(GameObject gameObject)
	{
		gameObject.SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("KnifeBlade"))
		{
			// Get the blade that is colliding.
			ButteredBladeController blade = other.GetComponent<ButteredBladeController>();

			// make sure its not null
			if (blade == null)
			{ return; }

			// check if we have butter on the blade
			if (blade.ButterOnBlade)
			{
				// remove the butter on the blade,
				// to ensure you need to get new butter for next sandwich
				blade.ButterOnBlade = false;

				// activate the butter on the sandwich
				ActivateButter();
			}

			knife = other.GetComponentInChildren<KnifeVelocity>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("KnifeBlade"))
		{
			knife = null;
		}
	}

	private void ActivateButter()
	{
		// get the FoodItem component to toogle Buttered to true.
		GetComponentInParent<FoodItem>().Buttered = true;
		buttered = true;
	}

	private void Reset(FoodItem food)
	{
		if(food == this)
		{
			buttered = false;
			butteredPercentage = 0;
			DeactivateAll();
		}
	}

	private void OnDisable()
	{
		FoodItem.Reset -= Reset;
	}
}
