using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButteredBladeController : MonoBehaviour // rename this script to something better
{
	// the object that is our butter object on the knife.
	[SerializeField] private GameObject butterOnKnife;
	
	private bool butterOnBlade;

	public bool ButterOnBlade
	{
		get 
		{ 
			return butterOnBlade; 
		} 
		set 
		{ 
			// set if we have butter on the blade to either true or false
			butterOnBlade = value;

			// set if our butter should be displayed on the knife.
			butterOnKnife.SetActive(value);
		}
	}
}
