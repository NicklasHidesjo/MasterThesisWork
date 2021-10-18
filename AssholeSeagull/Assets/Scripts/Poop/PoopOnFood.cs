using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopOnFood : MonoBehaviour
{
	FoodPackage foodPackage;
	[SerializeField] AudioClip poopOnFoodSound;


	void Start()
	{
		foodPackage = GetComponentInParent<FoodPackage>();
	}

	void OnTriggerEnter(Collider other)
	{
		GameObject hittedPoop = other.gameObject;
		if (other.gameObject.tag == "Poop") // use compare tag
		{
			// get our soundSingleton and play the sound (make into a variable)
			FindObjectOfType<SoundSingleton>().SeagullFx(poopOnFoodSound);

			// set that there is shit on the package
			// there is double code somewhere setting this,
			// i think its in the foodpackage itself.
			foodPackage.ShitOnPackage = true;

			// destroy the object hitting us, again 
			// i believe there is double code here in foodpackage 
			// and that its not needed here, or in food package as we 
			// always destroy the poop when it hits something.
			Destroy(hittedPoop);
		}
	}
}
