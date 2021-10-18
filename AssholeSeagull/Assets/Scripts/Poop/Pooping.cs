using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooping : MonoBehaviour
{
    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private Transform spawnPosition;

    public void Poop()
    {
        // creates a new poop.
        Instantiate(poopPrefab, spawnPosition.transform.position, Quaternion.identity);
    }
}
