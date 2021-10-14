using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterBlade : MonoBehaviour
{
	private bool butterOnBlade;
	[SerializeField] GameObject butterOnKnife;

	public bool ButterOnBlade
	{
		get { return butterOnBlade; } 
		set 
		{ 
			butterOnBlade = value;
			butterOnKnife.SetActive(value);
		}
	}
}
