using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeactivator : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Food"))
		{
			other.GetComponentInParent<FoodItem>().DeactivateFood();
		}
		if(other.CompareTag("Head"))
		{
			other.GetComponent<LettuceHead>().Deactivate();
		}
		if(other.CompareTag("Tomato"))
		{
			other.GetComponent<Tomato>().DeactivateTomato();
		}
	}
}
