using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterActivator : MonoBehaviour
{
    [SerializeField] GameObject butter;


	[SerializeField] bool manualyTriggerButter;
	private void Update()
	{
		if(manualyTriggerButter)
		{
			ActivateButter();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("KnifeBlade"))
		{
			ButterBlade blade = other.GetComponent<ButterBlade>();
			if(blade == null) { return; }
			if(blade.ButterOnBlade)
			{
				blade.ButterOnBlade = false;
				ActivateButter();
			}
		}
	}

	private void ActivateButter()
	{
		butter.SetActive(true);
		GetComponentInParent<FoodItem>().Buttered = true;
		gameObject.SetActive(false);
	}
}
