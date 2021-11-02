using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScore : MonoBehaviour
{
    FoodItem food;
    FoodSettings foodSettings;

	private void Start()
	{
        food = GetComponent<FoodItem>();
        foodSettings = food.FoodSettings;

	}

	public int GetScore()
    {
        if (food.IsSpoiled)
        {
            return -foodSettings.spoiledPunishment;
        }
        if (food.PoopOnFood)
        {
            return -foodSettings.poopPunishment;
        }

        int score = foodSettings.baseScore;

        if (food.FoodAbove == foodSettings.worstAbove)
        {
            score -= foodSettings.worstIngredientPunishment;
        }
        if (food.FoodBelow == foodSettings.worstBelow)
        {
            score -= foodSettings.worstIngredientPunishment;
        }

        if (food.FoodType != food.FoodAbove && food.FoodAbove != FoodTypes.None)
        {
            if (food.FoodType != food.FoodAboveAbove && food.FoodAboveAbove != FoodTypes.None && food.FoodAboveAbove != food.FoodAbove)
            {
                score += foodSettings.varietyBonus;
            }
        }

        if (food.FoodType != food.FoodBelow && food.FoodBelow != FoodTypes.None)
        {
            if (food.FoodType != food.FoodBelowBelow && food.FoodBelowBelow != FoodTypes.None && food.FoodBelowBelow != food.FoodBelow)
            {
                score += foodSettings.varietyBonus;
            }
        }

        return score;
    }
}
