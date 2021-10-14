using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullManager : MonoBehaviour
{
    //Food Packages
    [SerializeField] Transform breadPackage;
    [SerializeField] Transform cheesePackage;
    [SerializeField] Transform hamPackage;

    SeagullMovement seagullMovement;
    [SerializeField] ScareBird scareBird;

    [SerializeField] Transform endFlightOne;
    [SerializeField] Transform endFlightTwo;
    [SerializeField] Transform endFlightTree;

    [SerializeField] Transform seagullSpawnPointsOne;
    [SerializeField] Transform seagullSpawnPointsTwo;
    [SerializeField] Transform seagullSpawnPointsTree;

    int randomSpawnPoint;

    [SerializeField] SeagullMovement seagullPrefab;

    int currentNumberOfSeagulls = 0;
    int maxNumberOfSeagulls = 1;

    [SerializeField] float spawnIntervalls = 5f;

    private void OnEnable()
    {
        StartCoroutine("SpawnSeagull");
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Despawn(GameObject seagull)
    {
        Destroy(seagull);
        currentNumberOfSeagulls--;
    }

    IEnumerator SpawnSeagull()
    {
        while(true)
        {       
            if (currentNumberOfSeagulls < maxNumberOfSeagulls)
            {
                yield return new WaitForSeconds(spawnIntervalls);

                randomSpawnPoint = Random.Range(0, 3);

                if (randomSpawnPoint == 0)
                {
                    seagullMovement = Instantiate(seagullPrefab, seagullSpawnPointsOne.position, Quaternion.identity);
                    RandomEndPoint();
                }
                else if(randomSpawnPoint == 1)
                {
                    seagullMovement = Instantiate(seagullPrefab, seagullSpawnPointsTwo.position, Quaternion.identity);
                    RandomEndPoint();
                }
                else if(randomSpawnPoint ==2)
                {
                    seagullMovement = Instantiate(seagullPrefab, seagullSpawnPointsTree.position, Quaternion.identity);
                    RandomEndPoint();
                }

                scareBird.seagullMovement = seagullMovement;

                seagullMovement.BreadPackage = breadPackage;
                seagullMovement.HamPackage = hamPackage;
                seagullMovement.CheesePackage = cheesePackage;

                seagullMovement.seagullManager = this;

                currentNumberOfSeagulls++;

                seagullMovement.Init();
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void RandomEndPoint()
    {
        randomSpawnPoint = Random.Range(0, 3);

        if (randomSpawnPoint == 0)
        {
            seagullMovement.flightEnd = endFlightOne;
        }
        else if(randomSpawnPoint == 1)
        {
            seagullMovement.flightEnd = endFlightTwo;
        }
        else if(randomSpawnPoint == 2)
        {
            seagullMovement.flightEnd = endFlightTree;
        }
    }
}
