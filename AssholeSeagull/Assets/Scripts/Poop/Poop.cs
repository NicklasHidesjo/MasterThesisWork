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
        Destroy(gameObject);
    }
}
