using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterPackage : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("KnifeBlade"))
		{
			ButterBlade blade = other.GetComponent<ButterBlade>();
			if(blade == null) { return; }
			blade.ButterOnBlade = true;
		}
	}
}
