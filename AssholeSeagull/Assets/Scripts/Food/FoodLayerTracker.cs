using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLayerTracker : MonoBehaviour
{
    // the distance that a ray gets cast to see what is above and below it.
    [SerializeField] private float rayDistance;

    [SerializeField] private LayerMask foodLayer;

    FoodItem food;

    void Start()
    {
        food = GetComponent<FoodItem>();
    }

    public void SetFoodAboveAndBelow()
    {
        // create a null RaycastHit object.
        RaycastHit hit;

        // create our 2 directions
        Vector3 northSide = new Vector3(0, rayDistance, 0);
        Vector3 southSide = new Vector3(0, -rayDistance, 0);

        // debug draw those rays so we see them (remove this?)
        Debug.DrawRay(transform.position, northSide, Color.blue);
        Debug.DrawRay(transform.position, southSide, Color.red);

        // cast the ray in one direction, and see if we hit something
        if (Physics.Linecast(transform.position, transform.position + northSide, out hit, foodLayer))
        {
            // get the FoodItem we hit
            FoodItem foodHit = hit.collider.gameObject.GetComponent<FoodItem>();

            // check that it's not null
            if (food == null)
            {
                // set foodAbove and foodAboveAbove to none if null
                food.FoodAbove = FoodTypes.None;
                food.FoodAboveAbove = FoodTypes.None;
                // return/exit and don't run any code below
                return;
            }
            // set foodAbove to be that of the FoodItem's(food) foodtype
            food.FoodAbove = foodHit.FoodType;
            // set the foodAboveAbove to be that of the FoodItem's(food) FoodAbove.
            food.FoodAboveAbove = foodHit.FoodAbove;
        }
        else // if we did not hit anything 
        {
            // set both foodAbove and foodAboveAbove to none.
            food.FoodAbove = FoodTypes.None;
            food.FoodAboveAbove = FoodTypes.None;
        }

        // cast the ray in the other direction and do the same as for the above code.
        if (Physics.Linecast(transform.position, transform.position + southSide, out hit, foodLayer))
        {
            FoodItem foodHit = hit.collider.gameObject.GetComponent<FoodItem>();

            if (food == null)
            {
                food.FoodBelow = FoodTypes.None;
                food.FoodBelowBelow = FoodTypes.None;
                return;
            }

            food.FoodBelow = foodHit.FoodType;
            food.FoodBelowBelow = foodHit.FoodBelow;
        }
        else
        {
            food.FoodBelow = FoodTypes.None;
            food.FoodBelowBelow = FoodTypes.None;
        }
    }
}
