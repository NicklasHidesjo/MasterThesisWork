using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterActivator : MonoBehaviour
{
	// our butter object that we will toggle.
    [SerializeField] GameObject butter;

	private void OnTriggerEnter(Collider other)
	{
		// check if we have the right tag on the object colliding before going any further
		if(other.CompareTag("KnifeBlade"))
		{
			// Get the blade that is colliding.
			ButterBlade blade = other.GetComponent<ButterBlade>();

			// make sure its not null
			if(blade == null) { return; }

			// check if we have butter on the blade
			if(blade.ButterOnBlade)
			{
				// remove the butter on the blade,
				// to ensure you need to get new butter for next sandwich
				blade.ButterOnBlade = false;

				// activate the butter on the sandwich
				ActivateButter();
			}
		}
	}

	private void ActivateButter()
	{
		// activate the butter object
		butter.SetActive(true);

		// get the FoodItem component to toogle Buttered to true.
		GetComponentInParent<FoodItem>().Buttered = true;

		// deactivate this game object so that we won't trigger activateButter again
		gameObject.SetActive(false);
	}
}
