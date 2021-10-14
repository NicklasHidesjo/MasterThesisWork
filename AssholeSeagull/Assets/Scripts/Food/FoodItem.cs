using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class FoodItem : MonoBehaviour
{
    [SerializeField] AudioClip poopOnFoodSound;
    [SerializeField] float velocityThreshold = 0.01f;

    [SerializeField] private float spoilTime;
    [SerializeField] private float selfDestructTime;

    private GameManager gameManager;

    [SerializeField] GameObject spoiledParticles;

    [SerializeField] private float timer;
    [SerializeField] private bool isSpoiled;
    [SerializeField] private bool onPlate;
    [SerializeField] Material spoiledMaterial;
    [SerializeField] Material poopedMaterial;

    private bool inHand;

    private bool onSandwich;
    [SerializeField] private bool poopOnFood;
    private bool inPackage;
    private bool buttered;
    bool alreadySpoiled = false;

    [SerializeField] float rayDistance;


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

    FoodTypes foodAbove;
    FoodTypes foodAboveAbove;
    [Header("")]
    [SerializeField] FoodTypes worstAbove;

    FoodTypes foodBelow;
    FoodTypes foodBelowBelow;
    [Header("")]
    [SerializeField] FoodTypes worstBelow;

    private Rigidbody body;

    public FoodTypes FoodType => foodType;
    public FoodTypes FoodAbove => foodAbove;
    public FoodTypes FoodBelow => foodBelow;

    Interactable interactable;

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
        body = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        interactable = GetComponent<Interactable>();
    }

    private void Update()
	{
		RaycastFoodLayer();
        inHand = interactable.attachedToHand;

        if(gameManager.FreeRoam)
        {
            return;
        }

		if (onSandwich || inHand || inPackage|| onPlate)
		{
			return;
		}

		if (IsMoving())
		{
			return;
		}

		timer += Time.deltaTime;
		isSpoiled = timer > spoilTime;

		if (isSpoiled && !alreadySpoiled && !onPlate)
		{
			ChangeMaterial(spoiledMaterial);
		}

		if (timer > selfDestructTime && !onPlate)
		{
			Destroy(gameObject);
		}
	}

	public bool IsMoving()
	{
		return body.velocity.sqrMagnitude > velocityThreshold;
	}

	void ChangeMaterial(Material material)
    {
        alreadySpoiled = true;
        gameObject.GetComponent<Renderer>().material = material;
        spoiledParticles.SetActive(true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Poop")
        {
            FindObjectOfType<SoundSingleton>().PoopOnFood(poopOnFoodSound);
            GameObject poop = collider.gameObject;

            PoopOnFood = true;

            Destroy(poop);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Plate")
        {
            timer = 0f;
            gameManager.score--;
            onPlate = false;
            Debug.Log("Score: " + gameManager.score);
        }
    }

    public int GetScore()
    {
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
        RaycastHit hit;

        Vector3 northSide = new Vector3(0, rayDistance, 0);
        Vector3 southSide = new Vector3(0, -rayDistance, 0);

        Debug.DrawRay(transform.position, northSide, Color.blue);
        Debug.DrawRay(transform.position, southSide, Color.red);

        if(Physics.Linecast(transform.position, transform.position + northSide, out hit, foodLayer))
        {
            FoodItem food = hit.collider.gameObject.GetComponent<FoodItem>();

            if (food == null)
            {
                foodAbove = FoodTypes.None;
                return;
            }

            foodAbove = food.FoodType;
            foodAboveAbove = food.FoodAbove;
        }
        else
        {
            foodAbove = FoodTypes.None;
            foodAboveAbove = FoodTypes.None;
        }

        if (Physics.Linecast(transform.position, transform.position + southSide, out hit, foodLayer))
        {
            FoodItem food = hit.collider.gameObject.GetComponent<FoodItem>();

            if (food == null)
            {
                foodBelow = FoodTypes.None;
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
