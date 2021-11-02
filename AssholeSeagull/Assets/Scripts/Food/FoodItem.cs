using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;


public delegate void AddFood(FoodItem food);
public delegate void RemoveFood(FoodItem food);

// make scriptable object to save settings over the multiple scripts that
// are and shared between all food items.

public class FoodItem : MonoBehaviour
{
    // events for foodtracker
    public static event AddFood AddFood;
    public static event RemoveFood RemoveFood;

    // the material for it being spoiled
    [Header("Food Ruined Settings")] // create a FoodPoopHandler
    [SerializeField] private GameObject goneBadParticles; // change to a particle system that we turn on/off emission on instead?
    [SerializeField] private FoodSettings foodSettings;

    private FoodTypes foodAbove;
    private FoodTypes foodAboveAbove;
    private FoodTypes foodBelow;
    private FoodTypes foodBelowBelow;

    // booleans
    private bool isSpoiled = false;
    private bool poopOnFood = false;

    private bool onPlate = false;

    private bool inPackage = false;
    private bool inHand = false;
    private bool moving = false;
    private bool stolen = false;

    private bool buttered = false;

    private Interactable interactable;
    private Rigidbody rb;

    public FoodSettings FoodSettings
    {
        get
        {
            return foodSettings;
        }
    }

    // properties for each bool (look into making them into less or easier ones)
    public bool IsSpoiled
    {
        get
        {
            return isSpoiled;
        }
        set
        {
            if(isSpoiled == value)
			{
                return;
			}
            
            isSpoiled = value;

            if (value)
            {
                ChangeMaterial(foodSettings.spoiledMaterial);
            }
            CallFoodTrackingEvents();
        }
    }
    public bool PoopOnFood
    {
        get
        {
            return poopOnFood;
        }
        set
        {
            if (poopOnFood == value)
			{
                return;
			}
            poopOnFood = value;

            if (value)
            {
                ChangeMaterial(foodSettings.poopedMaterial);
            }
            CallFoodTrackingEvents();
        }
    }

    public bool OnPlate
    {
        get
        {
            return onPlate;
        }
        set
        {
            if(onPlate == value)
			{
                return;
			}
            onPlate = value;
            CallFoodTrackingEvents();
        }
    }

    public bool InPackage
    {
        get
        {
            return inPackage;
        }
        set
        {
            if(inPackage = value)
			{
                return;
			}
            inPackage = value;
            CallFoodTrackingEvents();
        }
    }
    public bool InHand
    {
        get
        {
            return inHand;
        }
        set
        {
            if(inHand == value)
			{
                return;
			}
            inHand = value;
            CallFoodTrackingEvents();
        }
    }
    public bool Moving
    {
        get
        {
            return moving;
        }
        set
        {
            moving = value;
        }
    }
    public bool Stolen
	{
        get
		{
            return stolen;
		}
        set
		{
            stolen = value;
        }
	}
    public bool Buttered
    {
        get
        {
            return buttered;
        }
        set
        {
            buttered = value;
        }
    }

    // properties for food
    public FoodTypes FoodType
    {
        get
        {
            return foodSettings.foodType;
        }
    }
    public FoodTypes FoodAbove
    {
        get
        {
            return foodAbove;
        }
        set
        {
            foodAbove = value;
        }
    }
    public FoodTypes FoodAboveAbove
    {
        get
        {
            return foodAboveAbove;
        }
        set
        {
            foodAboveAbove = value;
        }
    }
    public FoodTypes FoodBelow
    {
        get
        {
            return foodBelow;
        }
        set
        {
            foodBelow = value;
        }
    }
    public FoodTypes FoodBelowBelow
    {
        get
        {
            return foodBelowBelow;
        }
        set
        {
            foodBelowBelow = value;
        }
    }

    private void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    private void CallFoodTrackingEvents() // name this better.
    {
        if (inHand || poopOnFood || isSpoiled || inPackage || stolen)
        {
            RemoveFood?.Invoke(this);
        }
        else
        {
            AddFood?.Invoke(this);
        }
    }

    private void FixedUpdate()
    {
        // check if we are picked up or not
        InHand = interactable.attachedToHand;

        if(inHand && rb.isKinematic)
        {
            KinematicToggle(false);
            InPackage = false;
        }
    }

    private void ChangeMaterial(Material material)
    {
        // get the renderer on our object and change its material.
        gameObject.GetComponent<Renderer>().material = material;

        // turn on the spoiled object so show particles.
        goneBadParticles.SetActive(true);
    }

    public void Init(string name, bool poopOnFood)
    {
        gameObject.name = name;
        PoopOnFood = poopOnFood;
        rb = GetComponent<Rigidbody>();
        KinematicToggle(true);
    }

    public void KinematicToggle(bool isKinematic)
    {
        rb.isKinematic = isKinematic;
    }
}
