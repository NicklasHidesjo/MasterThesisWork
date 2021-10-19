using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScore : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int baseScore;
    [Tooltip("The value that gets added when ingredients are not the same above or below (going down two ingredients)")]
    [SerializeField] private int varietyBonus;
    [Tooltip("The value that gets reduced from baseScore when a worst ingredient is above")]
    [SerializeField] private int worstIngredientPunishment;
    [Tooltip("Will set the score to the negative value of this (50 on here = -50 in score)")]
    [SerializeField] private int spoiledPunishment;
    [Tooltip("Will set the score to the negative value of this (50 on here = -50 in score)")]
    [SerializeField] private int poopPunishment;

    [SerializeField] private FoodTypes worstAbove;
    [SerializeField] private FoodTypes worstBelow;

    FoodItem food;

	private void Start()
	{
        food = GetComponent<FoodItem>();
	}

	public int GetScore()
    {
        if (food.IsSpoiled)
        {
            return -spoiledPunishment;
        }
        if (food.PoopOnFood)
        {
            return -poopPunishment;
        }

        int score = baseScore;

        if (food.FoodAbove == worstAbove)
        {
            score -= worstIngredientPunishment;
        }
        if (food.FoodBelow == worstBelow)
        {
            score -= worstIngredientPunishment;
        }

        if (food.FoodType != food.FoodAbove && food.FoodAbove != FoodTypes.None)
        {
            if (food.FoodType != food.FoodAboveAbove && food.FoodAboveAbove != FoodTypes.None && food.FoodAboveAbove != food.FoodAbove)
            {
                score += varietyBonus;
            }
        }

        if (food.FoodType != food.FoodBelow && food.FoodBelow != FoodTypes.None)
        {
            if (food.FoodType != food.FoodBelowBelow && food.FoodBelowBelow != FoodTypes.None && food.FoodBelowBelow != food.FoodBelow)
            {
                score += varietyBonus;
            }
        }

        return score;
    }
}
