using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullMovement : MonoBehaviour
{
	// this script will be rewriten using animation events'
	// proper state machines and audio managers
	// this script will be split into several smaller ones.

	// make each seagull have it's own audiosource to allow for spatial sound.

	// our state machine (will be changed into a finite statemachine instead of enums)
	State currentState;
	enum State
	{
		PoopingPackage,
		PoopingFood
	}

	// our animator
	[SerializeField] Animator seagullAnimator;


	public int randomPackage;

	public Transform flightEnd;
	public SeagullManager seagullManager;
	FoodTracker foodTracker;

	Pooping pooping;
	
	[Header("Sounds")]
	[SerializeField] AudioClip poopingSound;
	[SerializeField] AudioClip seagullSound;
	[SerializeField] AudioClip scaredSound;
	AudioPlayer soundSingleton;

	[Header("Speed settings")]
	[SerializeField] float speed = 10f;
	[SerializeField] float endSpeed = 5f;
	[SerializeField] float acceleration = 0.5f;
	[SerializeField] float deacceleration = 0.5f;
	[SerializeField] float minSpeed = 1f;

	public Vector3 targetPosition;
	
	public bool isPoopingTime = false;
	bool hasPooped = false;
	bool flyingAway = false;
	public bool inDistance = false;
	bool isScared = false;

	float poopingTimer;
	[Header("Transforms")]
	//Food Packages 
	// remove serializefield and look into making them get
	// a list of all packages in the scene.
	[SerializeField] Transform breadPackage;
	public Transform BreadPackage
	{
		get
		{
			return breadPackage;
		}
		set
		{
			breadPackage = value;
		}
	}

	[SerializeField] Transform cheesePackage;
	public Transform CheesePackage
	{
		get
		{
			return cheesePackage;
		}
		set
		{
			cheesePackage = value;
		}
	}

	[SerializeField] Transform hamPackage; 

	public Transform HamPackage
	{
		get
		{
			return hamPackage;
		}
		set
		{
			hamPackage = value;
		}
	}

	public void Init()
	{
		// gets our audioSource.
		soundSingleton = FindObjectOfType<AudioPlayer>();
		// plays a seagull clip.
		soundSingleton.SeagullFx(seagullSound);

		// sets the state our bird will get. this is the only place we set the state?
		int randomState = Random.Range(0, 2);
		if (randomState == 0)
		{
			currentState = State.PoopingFood;
		}
		else if (randomState == 1)
		{
			currentState = State.PoopingPackage;
		}
		if(currentState == State.PoopingPackage)
		{
			FoodTarget();
		}
		else if(currentState == State.PoopingFood)
		{
			FoodItemTarget();
		}

		// gets our pooping component.
		pooping = GetComponent<Pooping>();

		// rotates our bird to look at our targetPosition.
		transform.LookAt(targetPosition);
	}

	void Update()
	{       
		// moves our bird.
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

		// Checks if we have arrived at target.
		if (transform.position == targetPosition && !isPoopingTime && !isScared)
		{
			// sets our speed.
			speed = endSpeed; 
			// sets our animation
			seagullAnimator.SetBool("Pooping", true);
			// sets a bool 
			isPoopingTime = true; // this will probably be removed.
		}

		// checks if it should poop.
		if (isPoopingTime == true)
		{
			poopingTimer += Time.deltaTime;

			if (poopingTimer > 1f && !hasPooped)
			{
				// we get the soundsingleton instead of using the variable we saved (why???)
				FindObjectOfType<AudioPlayer>().PoopOnFood(poopingSound);

				//bird poops.
				pooping.Poop();

				// sets that we have pooped
				hasPooped = true;
				// sets a trigger in our animator
				seagullAnimator.SetTrigger("FlyAway");
				// sets a bool in our animator
				seagullAnimator.SetBool("Pooping", false);
			}

			// checks if its time to fly away
			if (poopingTimer > 2.8f && !flyingAway)
			{
				// sets new target point
				targetPosition = flightEnd.position;
				// rotates our bird towards that position.
				transform.LookAt(targetPosition);

				// sets flying away to true.
				flyingAway = true;
			}  
		}

		// checks if we are flying away
		if (flyingAway)
		{
			speed += acceleration * Time.deltaTime;
		}
		// what is this below and what does it do?? 
		else if (speed > minSpeed)
		{
			speed -= deacceleration * Time.deltaTime;
		}
		else if(speed < minSpeed)
		{
			speed = minSpeed;
		}

		// despawn our bird.
		if (transform.position == targetPosition && flyingAway)
		{ 
			seagullManager.Despawn(gameObject);
		}
	}

	void FoodItemTarget()
	{
		// gets the FoodTracker
		foodTracker = FindObjectOfType<FoodTracker>();
		// gets a FoodItem target 
		Transform target = foodTracker.GetRandomTarget(); // rename this to TryGetRandomTarget or something similar

		// check if our target is null.
		if(target == null)
		{
			Debug.Log("Target position is null");
			// get a package as target instead.
			FoodTarget();
			// return/exit and don't do any of the code below
			return;
		}

		// sets our targetPosition
		targetPosition = target.position;

		// checks if our targetPosition is 0
		if (targetPosition == Vector3.zero)
		{
			// get a package as target instead.
			FoodTarget();
			// return/exit and don't do any of the code below
			return;
		}

		// sets targetPosition.y to be that of our own position.y (to now make the bird swoop down for shitting)
		targetPosition.y = transform.position.y;
	}

	
	private void FoodTarget()
	{
		// get a random package
		randomPackage = Random.Range(0, 3); // change this to be dynamic with every package

		// sets target based on the randomPackage int
		if (randomPackage == 0)
		{
			targetPosition = new Vector3(breadPackage.position.x, transform.position.y, breadPackage.position.z);
		}
		else if (randomPackage == 1)
		{
			targetPosition = new Vector3(hamPackage.position.x, transform.position.y, hamPackage.position.z);
		}
		else if (randomPackage == 2)
		{
			targetPosition = new Vector3(cheesePackage.position.x, transform.position.y, cheesePackage.position.z);
		}
		else
		{
			Debug.LogError("No food was found!");
		}
	}

	public void Scared()
	{
		// checks if bird is pooping
		if(isPoopingTime)
		{
			// return/exit don't do any of the code below.
			return;
		}
		// check if not already scared?
		if(isScared == false)
		{
			// again using the findobject instead of our variable we saved?
			FindObjectOfType<AudioPlayer>().SeagullFx(scaredSound);
			// setting that we are scared
			isScared = true;
			// making our target position our endposition.
			targetPosition = flightEnd.position;
			// changes our rotation to look at the endpoint.
			transform.LookAt(targetPosition);
			// sets a bool for flying away.
			flyingAway = true;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		// this checks if we are in distance to scare with a collider? 
		// i do not think this will be used. im sure this won't be used.
		if(collider.gameObject.name == "ScareDistance" && !flyingAway)
		{
			inDistance = true;
		}
	}
}
