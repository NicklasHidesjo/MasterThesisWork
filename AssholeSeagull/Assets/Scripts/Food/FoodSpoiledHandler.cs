using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpoiledHandler : MonoBehaviour
{
    [Header("Spoil Settings")]
    [Tooltip("The time before the FoodItem spoils")]
    [SerializeField] private float spoilTime;
    [Tooltip("The time before the object will be destroyed needs to be more then spoilTime (they use the same timer)")]
    [SerializeField] private float selfDestructTime;
    [Tooltip("The threshold for when the object is considered moving")]
    [SerializeField] private float velocityThreshold = 0.1f;

    private bool alreadySpoiled = false;

    private FoodItem food;
    private Rigidbody body;

    // the timer to check if food is spoiled or should be destroyed.
    private float timer = 0;

    void Start()
    {
        if (GameManager.Settings.AlwaysFreshFood)
        {
            enabled = false;
        }

        food = GetComponent<FoodItem>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // check if any of these bools are true
        if (food.OnSandwich || food.InHand || food.InPackage || food.OnPlate)
        {
            // return/exit and don't run the code below
            return;
        }

        // check if we are moving
        if (IsMoving())
        {
            food.Moving = true;
            // return/exit and don't run the code below
            return;
        }
        else
		{
            food.Moving = false;
		}

        // check if our timer is larger then our selfDestructTime and that we are also not on the plate
        if (timer > selfDestructTime)
        {
            // destroy the foodItem
            Destroy(gameObject);
        }

        // set isSpoiled based on if our timer is larger then our spoilTime
        food.IsSpoiled = timer > spoilTime;

        // this code doesn't do anything as i forgot what i was writing the code for
        // (will remain until i remove or remember)
        if(food.IsSpoiled != alreadySpoiled)
		{
            alreadySpoiled = true;
		}

        // increase our timer
        timer += Time.deltaTime;
    }

    public bool IsMoving()
    {
        // return true if our rigidbody's(body) velocity is larger then our velocityThreshold.
        return body.velocity.sqrMagnitude > velocityThreshold;
    }
}
