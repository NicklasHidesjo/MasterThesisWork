using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullController : MonoBehaviour
{
	// this script will be rewriten using animation events'
	// proper state machines and audio managers
	// this script might be split into several smaller ones.

	// make each seagull have its own audiosource to allow for spatial sound.

	// our state machine (will be changed into a finite statemachine instead of enums)
	State currentState;
	enum State
	{
		PoopingPackage,
		PoopingFood
	}

	// our animator
	[SerializeField] Animator seagullAnimator;

	// make all of this private
	public int randomPackage;
	public Transform flightEnd;
	FoodTracker foodTracker;

	Pooping pooping;
	
	// this will be in a SeagullSoundManager script 
	[Header("Sounds")]
	[SerializeField] AudioClip poopingSound;
	[SerializeField] AudioClip seagullSound;
	[SerializeField] AudioClip scaredSound;
	AudioPlayer soundSingleton;
	AudioSource audioSource;

	// settings as these will be in a scriptable object (to be able to create different Seagulls with different speeds and such)
	[Header("Speed settings")]
	[SerializeField] float speed = 10f;
	[SerializeField] float endSpeed = 5f;
	[SerializeField] float acceleration = 0.5f;
	[SerializeField] float deacceleration = 0.5f;
	[SerializeField] float minSpeed = 1f;

	// make this private 
	public Vector3 targetPosition;
	
	// alot of these bools will be removed as we change to animation events.
	public bool isPoopingTime = false;
	bool hasPooped = false;
	bool flyingAway = false;
	public bool inDistance = false;
	bool isScared = false;

	float poopingTimer;
	[Header("Transforms")]

	private List<Transform> foodPackages = new List<Transform>();

	// remove these as we will only have a list that we set in the SeagullManager.
	public List<Transform> FoodPackages
	{
		get
		{
			return foodPackages;
		}
		set
		{
			foodPackages = value;
		}
	}

	public void Init()
    {
        // gets our audioSource.
        audioSource = GetComponent<AudioSource>();

        // gets our pooping component.
        pooping = GetComponent<Pooping>();

        // rotates our bird to look at our targetPosition.
        //transform.LookAt(targetPosition);

        ResetBird();
    }

    public void ResetBird()
    {
        isPoopingTime = false;
        hasPooped = false;
        flyingAway = false;
        inDistance = false;
        isScared = false;

        poopingTimer = 0;

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

    private void WaitingToBeRemoved()
    {
        // Checks if we have arrived at target.
        if (transform.position == targetPosition && !isPoopingTime && !isScared)
        {
            // sets a bool 
            isPoopingTime = true; // this will probably be removed.
        }

        // checks if it should poop.
        // this will be controlled in our animation and animation events.
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

        // despawn our bird.
        if (transform.position == targetPosition && flyingAway)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetAnimation(string animation)
    {
		seagullAnimator.ResetTrigger("Poop");
		seagullAnimator.ResetTrigger("Fly");
		seagullAnimator.ResetTrigger("FlyAway");

		seagullAnimator.SetTrigger(animation);
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

    public void SetTargetPos()
    {
		// get a random package
		randomPackage = Random.Range(0, foodPackages.Count);

		Transform targetTransform = foodPackages[randomPackage];
		targetPosition = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
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

	public void PlaySpawnSound()
    {
		audioSource.clip = seagullSound;
		audioSource.Play();
    }
}
