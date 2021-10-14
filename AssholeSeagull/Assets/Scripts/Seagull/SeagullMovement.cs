using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullMovement : MonoBehaviour
{
    State currentState;
    enum State
    {
        PoopingPackage,
        PoopingFood
    }
    [SerializeField] Animator seagullAnimator;

    public int randomPackage;
    public Transform flightEnd;

    public SeagullManager seagullManager;
    FoodTracker foodTracker;

    Pooping pooping;
    FoodItem foodItem;

    [SerializeField] AudioClip poopingSound;
    [SerializeField] AudioClip seagullSound;
    [SerializeField] AudioClip scaredSound;
    SoundSingleton soundSingleton;

    public Vector3 targetPosition;
    
    [SerializeField] float speed = 10f;
    [SerializeField] float endSpeed = 5f;
    [SerializeField] float acceleration = 0.5f;
    [SerializeField] float deacceleration = 0.5f;
    [SerializeField] float minSpeed = 1f;

    public bool isPoopingTime = false;
    bool hasPooped = false;
    bool flyingAway = false;
    public bool inDistance = false;
    bool isScared = false;

    float poopingTimer;

    //Food Packages
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
        soundSingleton = FindObjectOfType<SoundSingleton>();
        soundSingleton.SeagullFx(seagullSound);

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

        pooping = GetComponent<Pooping>();
        transform.LookAt(targetPosition);
    }

    void Update()
    {       
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //framme vid food
        if (transform.position == targetPosition && !isPoopingTime && !isScared)
        {
            speed = endSpeed; 
            seagullAnimator.SetBool("Pooping", true);
            isPoopingTime = true;
        }

        //Dags att bajsa
        if (isPoopingTime == true)
        {
            poopingTimer += Time.deltaTime;

            if (poopingTimer > 1f && !hasPooped)
            {
                FindObjectOfType<SoundSingleton>().PoopOnFood(poopingSound);

                pooping.Poop();
                hasPooped = true;
                seagullAnimator.SetTrigger("FlyAway");
                seagullAnimator.SetBool("Pooping", false);
            }

            //Dags att flyga iväg
            if (poopingTimer > 2.8f && !flyingAway)
            {
                targetPosition = flightEnd.position;
                transform.LookAt(targetPosition);

                flyingAway = true;
            }  
        }

        if (flyingAway)
        {
            speed += acceleration * Time.deltaTime;
        }
        else if (speed > minSpeed)
        {
            speed -= deacceleration * Time.deltaTime;
        }
        else if(speed < minSpeed)
        {
            speed = minSpeed;
        }

        //Despawna fågel
        if (transform.position == targetPosition && flyingAway)
        { 
            seagullManager.Despawn(gameObject);
        }
    }

    //FoodItems är target point
    void FoodItemTarget()
    {
        foodTracker = FindObjectOfType<FoodTracker>();
        Transform target = foodTracker.GetRandomTarget();

        if(target == null)
        {
            Debug.Log("Target position is null");
            FoodTarget();
            return;
        }

        targetPosition = target.position;

        if (targetPosition == Vector3.zero)
        {
            FoodTarget();
            return;
        }

        targetPosition.y = transform.position.y;
    }

    //FoodPackage är target point
    private void FoodTarget()
    {
        randomPackage = Random.Range(0, 3);

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

    //Spelare skrämmer fågel
    public void Scared()
    {
        if(isPoopingTime)
        {
            return;
        }

        if(isScared == false)
        {
            FindObjectOfType<SoundSingleton>().SeagullFx(scaredSound);
            isScared = true;
            targetPosition = flightEnd.position;
            transform.LookAt(targetPosition);

            flyingAway = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "ScareDistance" && !flyingAway)
        {
            inDistance = true;
        }
    }
}
