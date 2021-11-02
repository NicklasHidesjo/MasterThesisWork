using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FoodSettings")]
public class FoodSettings : ScriptableObject
{
    public FoodTypes foodType;
    public FoodTypes worstAbove;
    public FoodTypes worstBelow;

    [Header("Materials")]
    public Material spoiledMaterial;
    public Material poopedMaterial;


    [Header("Spoil settings")]
    [Tooltip("The time before the FoodItem spoils")]
    public float spoilTime;
    [Tooltip("The time before the object will be destroyed needs to be more then spoilTime (they use the same timer)")]
    public float selfDestructTime;
    [Tooltip("The threshold for when the object is considered moving & won't spoil")]
    public float velocityThreshold = 0.1f;

    [Header("Score settings")]
    public int baseScore;
    [Tooltip("The value that gets added when ingredients are not the same above or below (going down two ingredients)")]
    public int varietyBonus;
    [Tooltip("The value that gets reduced from baseScore when a worst ingredient is above")]
    public int worstIngredientPunishment;
    [Tooltip("Will set the score to the negative value of this (50 on here = -50 in score)")]
    public int spoiledPunishment;
    [Tooltip("Will set the score to the negative value of this (50 on here = -50 in score)")]
    public int poopPunishment;
}
