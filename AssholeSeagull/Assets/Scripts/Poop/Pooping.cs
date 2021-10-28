using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooping : MonoBehaviour
{
    // move this into Seagull controller
    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private Transform spawnPosition;

    public void Poop()
    {
        // creates a new poop.
        Instantiate(poopPrefab, spawnPosition.transform.position, Quaternion.identity);
    }
}
