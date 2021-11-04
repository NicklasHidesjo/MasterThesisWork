using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
	// this script will be rewriten using animation events
	// this script might be split into several smaller ones.

	private Animator animator;
	private GrabbyFeet grabbyFeet;
	private SeagullAudio seagullAudio;
	private SeagullSettings seagullSettings;

	private Vector3 targetPosition;
	
	private bool isScared;
	private Vector3 flightEnd;
	private Vector3 foodPackage;

	private FoodItem foodTarget;

	private FoodItem pickedUp;

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
		
	public SeagullAudio SeagullAudio
    {
        get
        {
            if (seagullAudio == null)
            {
				seagullAudio = GetComponent<SeagullAudio>();
            }
			return seagullAudio;
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
				seagullAudio.PlayScaredSound();
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

	public void Init()
    {
		animator = GetComponent<Animator>();
		grabbyFeet = GetComponentInChildren<GrabbyFeet>();
		seagullAudio = GetComponent<SeagullAudio>();
    }

    private void ResetBird()
    {
		isScared = false;
        seagullSettings.speed = 10f;

		if(pickedUp != null)
		{
			foodTarget = null;

			pickedUp.DeactivateFood();
			pickedUp = null;

			//grabbyFeet.SetFoodRB(null);
		}
    }

    public void MoveBird()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, seagullSettings.speed * Time.deltaTime);
    }

	public bool ArrivedAtTarget()
    {
		return transform.position == targetPosition;
    }

	public bool IsInAnimation(string animation)
    {
		return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
    }

    public void Deactivate()
    {
		ResetBird();
        gameObject.SetActive(false);
    }

    public void SetAnimation(string animation)
    {
		animator.ResetTrigger("Poop");
		animator.ResetTrigger("FlyAway");

		animator.SetTrigger(animation);
    }

	public void Accelerate()
    {
        seagullSettings.speed += seagullSettings.acceleration * Time.fixedDeltaTime;
    }

    public void Deaccelerate()
    {
        seagullSettings.speed -= seagullSettings.deacceleration * Time.fixedDeltaTime;
		seagullSettings.speed = Mathf.Clamp(seagullSettings.speed, seagullSettings.minSpeed, Mathf.Infinity);
    }

	public void LookAt()
    {
		transform.LookAt(targetPosition);
    }

    public void SetSpeed()
    {
		seagullSettings.speed = seagullSettings.endSpeed;
    }

	public void SetExitPos()
	{
		targetPosition = flightEnd;
	}

	public void SetPackagePos()
	{
		Vector3 packagePos = new Vector3(foodPackage.x, transform.position.y, foodPackage.z);
		targetPosition = packagePos;
	}

	public void SetFoodPos()
	{
		Vector3 position = foodTarget.transform.position;
		//position.y += transform.localScale.y / 2;
		targetPosition = position;
	}

	public void PickUpFood()
	{
		foodTarget.transform.parent = transform;
		foodTarget.Stolen = true;
		foodTarget.GetComponent<Rigidbody>().isKinematic = true;
		//grabbyFeet.SetFoodRB(foodTarget.GetComponent<Rigidbody>());
		pickedUp = FoodTarget;
	}
}