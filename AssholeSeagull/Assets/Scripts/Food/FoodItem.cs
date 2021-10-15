using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FoodItem : MonoBehaviour
{
    [SerializeField] AudioClip poopOnFoodSound; // remove this and have it in a audio player script.
    [Tooltip("The threshold for when the object is considered moving")]
    [SerializeField] float velocityThreshold = 0.01f; 
    

    [Tooltip("The time before the FoodItem spoils")]
    [SerializeField] private float spoilTime;
    [Tooltip("The time before the object will be destroyed needs to be more then spoilTime (they use the same timer)")]
    [SerializeField] private float selfDestructTime;

    // the distance that a ray gets cast to see what is above and below it.
    [SerializeField] float rayDistance;

    // the object where our spoiled particle system is.
    [SerializeField] GameObject spoiledParticles; // change to a particle system that we turn on/off emission on instead?
    // the material for it being spoiled
    [SerializeField] Material spoiledMaterial;
    // the material for having poop on it.
    [SerializeField] Material poopedMaterial;

    // the timer to check if food is spoiled or should be destroyed.
    float timer = 0;

    private GameManager gameManager;

    // a collection of different bools (look into reducing the number of bools needed)
    bool isSpoiled = false;
    bool onPlate = false;
    bool inHand = false;
    bool onSandwich = false;
    bool poopOnFood = false;
    bool inPackage = false;
    bool buttered = false;
    bool alreadySpoiled = false;



    [Header("Score Settings")]
    [SerializeField] int baseScore;
    [Tooltip("The value that gets added when ingredients are not the same above or below (going down two ingredients)")]
    [SerializeField] int varietyBonus;
    [Tooltip("The value that gets reduced from baseScore when a worst ingredient is above")]
    [SerializeField] int worstIngredientPunishment;
    [Tooltip("Will set the score to the negative value of this (50 on here = -50 in score)")]
    [SerializeField] int spoiledPunishment;
    [Tooltip("Will set the score to the negative value of this (50 on here = -50 in score)")]
    [SerializeField] int poopPunishment;

    [Header("FoodType Settings")]
    [SerializeField] LayerMask foodLayer;
    [SerializeField] FoodTypes foodType;
    [SerializeField] FoodTypes worstAbove;
    [SerializeField] FoodTypes worstBelow;

    FoodTypes foodAbove;
    FoodTypes foodAboveAbove;

    FoodTypes foodBelow;
    FoodTypes foodBelowBelow;

    private Rigidbody body;

    public FoodTypes FoodType => foodType;
    public FoodTypes FoodAbove => foodAbove;
    public FoodTypes FoodBelow => foodBelow;

    Interactable interactable;

    // properties for each bool (look into making them into less or easier ones)
    public bool InHand
    {
        get { return inHand; }
        set { inHand = value; }
    }
    public bool IsSpoiled
    {
        get { return isSpoiled; }
    }
    public bool OnPlate
    {
        get { return onPlate; }
        set { onPlate = value; }
    }
    public bool PoopOnFood
    {
        get { return poopOnFood; }
        set 
        { 
            poopOnFood = value; 

            if(value)
            {
                ChangeMaterial(poopedMaterial);
            }
        }
    }
    public bool InPackage
    {
        get { return inPackage; }
        set { inPackage = value; }
    }
    public bool Buttered
    {
        get { return buttered; }
        set { buttered = value; }
    }

    private void Start()
    {
        // getting some references (make these 3 lines into a method named approprietly)
        body = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        interactable = GetComponent<Interactable>();
    }

    private void Update()
	{
        // Check what food is below or above 
		RaycastFoodLayer(); // change name to something better

        // check if we are picked up or not
        inHand = interactable.attachedToHand; 

        // check if we are in FreeRoam
        if(gameManager.FreeRoam)
        {
            // return/exit and don't run the code below
            return;
        }

        // check if any of these bools are true
		if (onSandwich || inHand || inPackage|| onPlate)
		{
            // return/exit and don't run the code below
            return;
		}

        // check if we are moving
		if (IsMoving())
		{
			return;
		}

        // increase our timer
		timer += Time.deltaTime;

        // set isSpoiled based on if our timer is larger then our spoilTime
		isSpoiled = timer > spoilTime;

        // check if we are spoiled and we haven't already changed our material.
		if (isSpoiled && !alreadySpoiled && !onPlate) // remove !onPlate as if we are on plate we do not get here.
		{
            // change the material of the FoodItem
			ChangeMaterial(spoiledMaterial);
		}
        // check if our timer is larger then our selfDestructTime and that we are also not on the plate
		if (timer > selfDestructTime && !onPlate) // remove !onPlate here? as we won't get here if we are on plate. 
		{
            // destroy the foodItem
			Destroy(gameObject);
		}
	}

	public bool IsMoving()
	{
        // return true if our rigidbody's(body) velocity is larger then our velocityThreshold.
		return body.velocity.sqrMagnitude > velocityThreshold;
	}

	void ChangeMaterial(Material material)
    {
        // set already spoiled to true 
        alreadySpoiled = true;

        // get the renderer on our object and change its material.
        gameObject.GetComponent<Renderer>().material = material;

        // turn on the spoiled object so show particles.
        spoiledParticles.SetActive(true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // check if the object entering has the correct tag
        if(collider.gameObject.tag == "Poop") // change to compareTag
        {
            // play our audio.
            FindObjectOfType<SoundSingleton>().PoopOnFood(poopOnFoodSound);

            // get a reference to poop
            GameObject poop = collider.gameObject; // this seems unnecessary so maybe remove it?

            // set that we have poop on the food.
            PoopOnFood = true;

            // destroying the poop object
            Destroy(poop); // remove this and make sure that the poop that hits destroys itself?
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // dubbelcheck all of this and remove all that is not needed (might be everything)

        // check if´the trigger we are exiting has the correct tag. 
        if (collider.tag == "Plate")
        {
            // set the timer to 0
            timer = 0f;
            // remove score
            gameManager.score--;
            // set onPlate to false
            onPlate = false;
            
            Debug.Log("Score: " + gameManager.score);
        }
    }

    public int GetScore()
    {
        // move this into its own script on the food item to
        // make this script less cluttered.

        if (isSpoiled)
        {
            return -spoiledPunishment;
        }
        if (PoopOnFood)
        {
            return -poopPunishment;
        }
        
        int score = baseScore;
        
        if (foodAbove == worstAbove)
        {
            score -= worstIngredientPunishment;
        }
        if(foodBelow == worstBelow)
        {
            score -= worstIngredientPunishment;
        }

        if(foodType != foodAbove && foodAbove != FoodTypes.None)
        {
            if(foodType != foodAboveAbove && foodAboveAbove != FoodTypes.None && foodAboveAbove != foodAbove)
            {
                score += varietyBonus;
            }
        }

        if(foodType != foodBelow && foodBelow != FoodTypes.None)
        {
            if(foodType!= foodBelowBelow && foodBelowBelow != FoodTypes.None && foodBelowBelow != foodBelow)
            {
                score += varietyBonus;
            }
        }

        return score;
    }

    void RaycastFoodLayer()
    {
        //look into making this a script of it's own and refactor everything in here.



        // create a null RaycastHit object.
        RaycastHit hit;

        // create our 2 directions
        Vector3 northSide = new Vector3(0, rayDistance, 0);
        Vector3 southSide = new Vector3(0, -rayDistance, 0);

        // debug draw those rays so we see them (remove this?)
        Debug.DrawRay(transform.position, northSide, Color.blue);
        Debug.DrawRay(transform.position, southSide, Color.red);

        // cast the ray in one direction, and see if we hit something
        if(Physics.Linecast(transform.position, transform.position + northSide, out hit, foodLayer))
        {
            // get the FoodItem we hit
            FoodItem food = hit.collider.gameObject.GetComponent<FoodItem>();

            // check that it's not null
            if (food == null)
            {
                // set foodAbove and foodAboveAbove to none if null
                foodAbove = FoodTypes.None;
                foodAboveAbove = FoodTypes.None;
                // return/exit and don't run any code below
                return;
            }
            // set foodAbove to be that of the FoodItem's(food) foodtype
            foodAbove = food.FoodType;
            // set the foodAboveAbove to be that of the FoodItem's(food) FoodAbove.
            foodAboveAbove = food.FoodAbove;
        }
        else // if we did not hit anything 
        {
            // set both foodAbove and foodAboveAbove to none.
            foodAbove = FoodTypes.None;
            foodAboveAbove = FoodTypes.None;
        }

        // cast the ray in the other direction and do the same as for the above code.
        if (Physics.Linecast(transform.position, transform.position + southSide, out hit, foodLayer))
        {
            FoodItem food = hit.collider.gameObject.GetComponent<FoodItem>();

            if (food == null)
            {
                foodBelow = FoodTypes.None;
                foodBelowBelow = FoodTypes.None;
                return;
            }

            foodBelow = food.FoodType;
            foodBelowBelow = food.FoodBelow;
        }
        else
        {
            foodBelow = FoodTypes.None;
            foodBelowBelow = FoodTypes.None;
        }
    }
}
