using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [Tooltip("The maximum height that the sandwich can have (in unity units)")]
    [SerializeField] private float rayDistance;
    [Tooltip("The radius of detection of the ray from the center of the plate")]
    [SerializeField] private float rayRadius;
    [Tooltip("The layer that only food is on")]
    [SerializeField] private LayerMask foodLayer;

    // a list of everything that is on the plate/sandwich
    [SerializeField] private List<FoodItem> sandwichPieces = new List<FoodItem>();
    public List<FoodItem> SandwichPieces 
    { 
        get 
        { 
            return sandwichPieces; 
        } 
    }

    private void FixedUpdate()
    {
        // check if we have food on the plate.
        if (!FirstFoodOnPlate())
        {
            // return/exit and don't run any of the code below
            return;
        }

        // add food on the plate to our list
        AddFoodToList();
        // finish the sandwich
        FinishSandwich();
    }

    private bool FirstFoodOnPlate()
    {
        // create a null RaycastHit object.
        RaycastHit hit;

        // cast a ray in the direction of plateVector
        if (Physics.SphereCast(transform.position, rayRadius, transform.forward, out hit, rayDistance, foodLayer))
        {
            // get the collisions FoodItem(food)
            FoodItem food = hit.collider.gameObject.GetComponent<FoodItem>();

            // check if we can just return if food.Buttered.
            // move the null check down and just set
            // what food is in this linecast.

            // check if its null
            if(food == null)
            {
                return false;
            }

            // check if it's not of type bread
            if(food.FoodType != FoodTypes.Bread)
            {
                return false;
            }    
            
            // check if the bread is buttered
            if(food.Buttered)
            {
                return true;
            }
        }
        // if we do not hit anything on the foodLayed we return false
        return false;
    }

    private void AddFoodToList()
    {
        // Go trough all the items on the list.
		foreach (var food in sandwichPieces)
		{
            // set that they are not on the plate anymore
            food.OnPlate = false;
		}

        // overwrite the list with a new one (maybe just use clear here and remove all null objects if needed?) 
        sandwichPieces = new List<FoodItem>();

        // create an array to store all that our ray hits.
        RaycastHit[] hits;
        // cast a ray upwards to hit all on foodLayer that are above and store in our array.
        hits = Physics.SphereCastAll(transform.position, rayRadius, transform.forward, rayDistance, foodLayer);
        // check if we have more then 0 elements in our array.
        if(hits.Length > 0)
        {
            // with a for-each loop we go trough the entire array of hits.
			foreach (var hit in hits)
			{
                // we get the FoodItem
                FoodItem food = hit.collider.GetComponent<FoodItem>();
                // we check if it's moving or in one of our hands
                if (food.Moving || food.InHand)
                {
                    // continue on with the next element and skip this one.
                    continue;
                }
                // add the piece of food to our list of sandwichPieces
                sandwichPieces.Add(food);
                // set that the food is on the plate.
                food.OnPlate = true;
            }
        }
    }

    private void FinishSandwich()
    {
        bool finishedSandwich = false;
        // using a foreach loop go through each FoodItem in our sandwichPieces list
		foreach (var food in sandwichPieces)
		{
            // check if it's not bread
            if(food.FoodType != FoodTypes.Bread)
			{
                continue;
			}
            // check if it's buttered.
            if(food.Buttered)
			{
                continue;
			}

            finishedSandwich = true;
		}

        if(finishedSandwich)
        {
            foreach (var food in sandwichPieces)
            {
                food.GetComponent<FoodLayerTracker>().SetFoodAboveAndBelow();
            }
            FindObjectOfType<ScoreManager>().FinishSandwich(true);
        }
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rayRadius);
	}
}
