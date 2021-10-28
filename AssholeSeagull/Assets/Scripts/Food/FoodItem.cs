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
    
    [SerializeField] private Material spoiledMaterial;
    [SerializeField] private AudioClip poopOnFoodSound; // remove this and have it in a audio player script.
    // the material for having poop on it.
    [SerializeField] private Material poopedMaterial;

    [Header("FoodType Settings")]
    [SerializeField] private FoodTypes foodType;

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

    // properties for each bool (look into making them into less or easier ones)
    public bool IsSpoiled
    {
        get
        {
            return isSpoiled;
        }
        set
        {
            isSpoiled = value;
            if (value)
            {
                ChangeMaterial(spoiledMaterial);
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
            poopOnFood = value;

            if (value)
            {
                ChangeMaterial(poopedMaterial);
                FindObjectOfType<AudioPlayer>().PoopOnFood(poopOnFoodSound);
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
            return foodType;
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
        if (inHand || onPlate || poopOnFood || isSpoiled || inPackage || stolen)
        {
            RemoveFood?.Invoke(this);
        }
        else
        {
            AddFood?.Invoke(this);
        }
    }

    private void Update()
    {
        // check if we are picked up or not
        InHand = interactable.attachedToHand;
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
    }
}
