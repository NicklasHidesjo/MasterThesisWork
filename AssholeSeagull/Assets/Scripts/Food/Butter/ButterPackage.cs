using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterPackage : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		// check if we have the right tag on the object colliding before going any further
		if (other.CompareTag("KnifeBlade"))
		{
			// get the blade component
			ButterBlade blade = other.GetComponent<ButterBlade>();
			
			// check if the blade is null
			if(blade == null) { return; }

			// set the blade to have butter on it
			blade.ButterOnBlade = true;
		}
	}
}
