using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooping : MonoBehaviour
{
    [SerializeField] GameObject poopPrefab;
    [SerializeField] Transform spawnPosition;

    float despawnTimer; // remove unused variable

    public void Poop()
    {
        // creates a new poop.
        Instantiate(poopPrefab, spawnPosition.transform.position, Quaternion.identity);
    }
}
