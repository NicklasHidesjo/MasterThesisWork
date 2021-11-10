using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawnPoint : MonoBehaviour
{
    private LettuceSpawner lettuceSpawner;

    private void Awake()
    {
        lettuceSpawner = FindObjectOfType<LettuceSpawner>();
    }

    private void OnEnable()
    {
        if (GetComponentInChildren<FoodItem>() != null)
        {
            return;
        }
        else
        {
            FoodItem leaf = lettuceSpawner.GetLettuceLeaf();
            leaf.DeactivateFood();
            leaf.KinematicToggle(true);
            leaf.transform.parent = transform;
            leaf.transform.position = transform.position;
            leaf.transform.rotation = transform.rotation;
            leaf.gameObject.SetActive(true);
        }
    }

    public bool HasChild()
    {
        return GetComponentInChildren<FoodItem>() != null;
    }

    public void SmearShitOnLeaf()
	{
        FoodItem leaf = GetComponentInChildren<FoodItem>();
        if (leaf != null)
		{
            leaf.PoopOnFood = true;
		}
	}
}