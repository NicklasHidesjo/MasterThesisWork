using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooping : MonoBehaviour
{
    [SerializeField] GameObject poopPrefab;
    [SerializeField] Transform spawnPosition;

    float despawnTimer;

    public void Poop()
    {
        Instantiate(poopPrefab, spawnPosition.transform.position, Quaternion.identity);
    }
}
