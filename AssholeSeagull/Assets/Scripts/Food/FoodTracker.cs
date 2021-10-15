using System.Collections.Generic;
using UnityEngine;

public class FoodTracker : MonoBehaviour
{
    List<Transform> foodTransformList;

    public Transform GetRandomTarget()
    {
        // we might not need the list and might be able to just use the foodArray instead.


        // make sure that our foodTransformList is a new list
        foodTransformList = new List<Transform>();

        // find all objects with the tag "Food" and store them in an array
        GameObject[] foodArray = GameObject.FindGameObjectsWithTag("Food");

        // go trough the array
        foreach (GameObject food in foodArray)
        {
            // save them in our list.
            foodTransformList.Add(food.transform);
        }

        int randomFoodTarget;

        // check how many times we have already done our do-while loop.
        int currentIteration = 0;
        // the maximum number of times we are allowed to do the do-while loop before breaking.
        int maxIterations = 5;

        bool noTargetFound = false;

        do
        {
            // get a random target
            randomFoodTarget = Random.Range(0, foodTransformList.Count);

            // increase currentIteration
            currentIteration++;

            // check so that we haven't gone the maximum iteration amount
            if (currentIteration >= maxIterations)
            {
                // change to keep track that we didn't find a foodTarget.
                noTargetFound = true;
            }
        }
        while (GetInvalidTarget(foodTransformList[randomFoodTarget].GetComponent<FoodItem>()) && !noTargetFound);

        // check if we did not find a target
        if (noTargetFound)
        {
            Debug.Log("No target found returning null!");
            // return null 
            return null;
        }

        // if we did find a target to return we return that transform.
        return foodTransformList[randomFoodTarget];
    }

    private bool GetInvalidTarget(FoodItem randomFoodTarget)
    {
        // this method checks if the target that we have gotten is valid or not.

        if (randomFoodTarget.OnPlate)
        {
            return true;
        }
        if (randomFoodTarget.IsSpoiled)
        {
            return true;
        }
        if (randomFoodTarget.PoopOnFood)
        {
            return true;
        }

        return false;
    }


    // these methods below looks like they are not used for anything,
    // remove if that is the case
    public void AddFoodTransform(Transform foodTransform)
    {
        // adds a transform to the list.
        foodTransformList.Add(foodTransform);
    }

    public void RemoveFoodTransform(Transform foodTransform)
    {
        // removes a transform from the list.
        foodTransformList.Remove(foodTransform);
    }
}
