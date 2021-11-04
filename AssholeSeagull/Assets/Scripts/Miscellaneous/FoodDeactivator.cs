using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDeactivator : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Food"))
		{
			other.GetComponent<FoodItem>().DeactivateFood();
		}
	}
}
