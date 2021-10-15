using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    [Tooltip("The maximum height that the sandwich can have (in unity units)")]
    [SerializeField] float rayDistance = 100f;
    [Tooltip("The layer that only food is on")]
    [SerializeField] LayerMask foodLayer;

    // a list of everything that is on the plate/sandwich
    List<FoodItem> sandwichPieces = new List<FoodItem>();
    public List<FoodItem> SandwichPieces { get { return sandwichPieces; } }

    bool sandwichIsFinished;


    private void Update()
    {
        // check if we have finished the sandwich
        if (sandwichIsFinished)
        {
            // return/exit and don't run any of the code below
            return;
        }

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

    bool FirstFoodOnPlate()
    {
        // get the direction to cast the ray
        Vector3 plateVector = new Vector3(0, rayDistance, 0);
        // create a null RaycastHit object.
        RaycastHit hit;

        // cast a ray in the direction of plateVector
        if (Physics.Linecast(transform.position, transform.position + plateVector, out hit, foodLayer))
        {
            // get the collisions FoodItem(food)
            FoodItem food = hit.collider.gameObject.GetComponent<FoodItem>();

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

    void AddFoodToList()
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
        hits = Physics.RaycastAll(transform.position, transform.forward, rayDistance, foodLayer);
        
        // check if we have more then 0 elements in our array.
        if(hits.Length > 0)
        {
            // with a for-loop we go trough the entire array of hits.
            for (int i = 0; i < hits.Length; i++)
            {
                // we get the FoodItem
                FoodItem food = hits[i].collider.GetComponent<FoodItem>();

                // we check if it's moving or in one of our hands
                if(food.IsMoving() || food.InHand)
				{
                    // continue on with the next element and skip this one.
                    continue;
				}
                // add the piece of food to our list of sandwichPieces
                sandwichPieces.Add(food);
                // set that the food is on the plate.
                food.OnPlate = true;
            } // look into making this a foreach instead
        }

        Debug.DrawRay(transform.position, transform.forward, Color.blue);
    }

    void FinishSandwich()
    {
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

            // change the bool that it's finished to true
            sandwichIsFinished = true; // this bool might not be needed.

            // make a call to FinishSandwich on the GameManager.
            FindObjectOfType<GameManager>().FinishSandwich(true);
		}
    }
}
