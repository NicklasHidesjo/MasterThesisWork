using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Scared();
public class SeagullController : MonoBehaviour
{
	// this script will be rewriten using animation events
	// this script might be split into several smaller ones.

	[SerializeField] GameObject foodCollider;

	public static event Scared Scared;

	private GrabbyFeet grabbyFeet; // use this in states instead if we will use it.

	private SeagullSettings seagullSettings;
	
	private bool isScared;
	private Vector3 flightEnd;
	private Vector3 foodPackage;

	private FoodItem foodTarget;

	public SeagullSettings SeagullSettings
    {
		get
        {
			return seagullSettings;
        }

		set
        {
			seagullSettings = value;
        }
    }
	public bool IsScared
    {
		get 
		{ 
			return isScared; 
		}
		set 
		{ 
			if(value)
            {
				Scared?.Invoke();
			}
			isScared = value; 
		}
    }
	public Vector3 FlightEnd
    {
		get 
		{ 
			return flightEnd; 
		}
        set 
		{ 
			flightEnd = value; 
		}
    }
	public Vector3 FoodPackage
	{
		get 
		{ 
			return foodPackage; 
		}
		set 
		{ 
			foodPackage = value; 
		}
	}	
	public FoodItem FoodTarget
	{
		get
		{
			return foodTarget;
		}
		set
		{
			foodTarget = value;
		}
	}

	public bool SetFoodCollider
    {
        private get
        {
			return foodCollider.activeSelf;
        }
        set
        {
			foodCollider.SetActive(value);
        }
    }
}