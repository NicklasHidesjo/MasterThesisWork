using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butter : MonoBehaviour
{
	float butteringDone;

	[Tooltip("The threshold for each butter-stage to be initiated at.")]
	[SerializeField] List<float> butterStageInitiation;

	[Tooltip("The objects to be toggled on/off based on the buttered amount")]
	[SerializeField] GameObject[] butterObjects;

	ButterVelocity knife;

	private void Update()
	{
		if (knife == null) { return; }

		// increase the amount that is buttered.
		butteringDone += knife.Velocity;

		ChangeButterStage();
	}

	private void ChangeButterStage()
	{
		// make the guts of this into one or two methods?
		// One for deactivating all and one for activating the one we want,
		// using parameters in the activation method.
		if (butteringDone > butterStageInitiation[1] && !butterObjects[2].activeSelf)
		{
			butterObjects[0].SetActive(false);
			butterObjects[1].SetActive(true);
			butterObjects[2].SetActive(false);

		}
		else if (butteringDone > butterStageInitiation[0] && !butterObjects[1].activeSelf)
		{
			butterObjects[0].SetActive(false);
			butterObjects[1].SetActive(false);
			butterObjects[2].SetActive(true);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		// do i need this null check? (seeing as i check for null in update)
		if(other.gameObject.GetComponent<ButterBlade>())
		{
			knife = other.GetComponentInChildren<ButterVelocity>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		// here i do need it as i need to know if you exit with the knife or not.
		if(other.gameObject.GetComponent<ButterBlade>())
		{
			knife = null;
		}
	}
}
