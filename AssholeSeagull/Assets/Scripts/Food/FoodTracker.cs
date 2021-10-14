using System.Collections.Generic;
using UnityEngine;

public class FoodTracker : MonoBehaviour
{
    [SerializeField] List<Transform> foodTransformList;

    public Transform GetRandomTarget()
    {
        foodTransformList = new List<Transform>();
        GameObject[] foodArray = GameObject.FindGameObjectsWithTag("Food");

        foreach (GameObject food in foodArray)
        {
            foodTransformList.Add(food.transform);
        }

        int randomFoodTarget;
        int currentIteration = 0;
        int maxIterations = 5;

        bool noTargetFound = false;

        do
        {
            randomFoodTarget = Random.Range(0, foodTransformList.Count);
            currentIteration++;

            if (currentIteration >= maxIterations)
            {
                noTargetFound = true;
            }
        }
        while (GetInvalidTarget(foodTransformList[randomFoodTarget].GetComponent<FoodItem>()) && !noTargetFound);

        if (noTargetFound)
        {
            Debug.Log("Returning null");
            return null;
        }

        return foodTransformList[randomFoodTarget];
    }

    private bool GetInvalidTarget(FoodItem randomFoodTarget)
    {
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

    public void AddFoodTransform(Transform foodTransform)
    {
        foodTransformList.Add(foodTransform);
    }

    public void RemoveFoodTransform(Transform foodTransform)
    {
        foodTransformList.Remove(foodTransform);
    }
}
