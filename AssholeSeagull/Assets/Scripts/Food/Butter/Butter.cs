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

	private void Update()
	{
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
		// do i need this null check? (seeing as i check for null in update)
		if(other.gameObject.GetComponent<ButteredBladeController>())
		{
			knife = other.GetComponentInChildren<KnifeVelocity>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.GetComponent<ButteredBladeController>())
		{
			knife = null;
		}
	}
}
