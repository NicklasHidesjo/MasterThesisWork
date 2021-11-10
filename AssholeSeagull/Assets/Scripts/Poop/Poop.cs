using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("FoodPackage"))
		{
			collision.gameObject.GetComponentInParent<FoodPackage>().ShitInPackage = true;
		}
		if(collision.gameObject.CompareTag("Tomato"))
		{
			collision.gameObject.GetComponent<Tomato>().ShitOn = true;
		}
		if (collision.gameObject.CompareTag("Head"))
		{
			collision.gameObject.GetComponent<LettuceHead>().GotShitOn();
		}
		Destroy(gameObject);
	}
}
