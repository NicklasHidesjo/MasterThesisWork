using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
	// this script will be rewriten using animation events'
	// Make audio manager
	// this script might be split into several smaller ones.

	private Animator animator;
	
	// this will be in a SeagullSoundManager script 
	[Header("Sounds")]
	[SerializeField] private AudioClip poopingSound; // move this to pooping script
    [SerializeField] private AudioClip seagullSound;
	[SerializeField] private AudioClip scaredSound;
	private AudioSource audioSource;

	// settings as these will be in a scriptable object (to be able to create different Seagulls with different speeds and such)
	[Header("Speed settings")]
	[SerializeField] private float speed = 10f;
	[SerializeField] private float endSpeed = 5f;
	[SerializeField] private float acceleration = 0.5f;
	[SerializeField] private float deacceleration = 0.5f;
	[SerializeField] private float minSpeed = 1f;

	private Vector3 targetPosition;
	
	private bool isScared;
	private Vector3 flightEnd;
	private Vector3 foodPackage;
		
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
				PlayScaredSound();
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

	public void Init()
    {
        // gets our audioSource.
        audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
    }

    public void ResetBird()
    {
		isScared = false;
        speed = 10f;
    }

    public void MoveBird()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
        speed += acceleration * Time.deltaTime;
    }

    public void Deaccelerate()
    {
        speed -= deacceleration * Time.deltaTime;
		speed = Mathf.Clamp(speed, minSpeed, Mathf.Infinity);
    }

	public void LookAt()
    {
		transform.LookAt(targetPosition);
    }

    public void SetSpeed()
    {
        speed = endSpeed;
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

    private void PlayScaredSound()
    {
        audioSource.clip = scaredSound;
        audioSource.Play();
    }

    public void PlaySpawnSound()
    {
		audioSource.clip = seagullSound;
		audioSource.Play();
    }
}