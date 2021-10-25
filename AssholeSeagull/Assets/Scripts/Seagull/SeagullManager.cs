using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullManager : MonoBehaviour
{
	// this script will be rewritten and changed, to a great extent

    // make a object pool to have our seagulls in (so we don't have to destroy and reload them all the time)

    // remove the corutine. Have a separate script for keeping track of when to spawn, use event in that script
    // that SeagullManager subscribes to.

    //Food Packages
	// turn this into a list of targetPoints.
    [SerializeField] Transform breadPackage;
    [SerializeField] Transform cheesePackage;
    [SerializeField] Transform hamPackage;

	// why do we have this?
	// it should be made a reference only in the method that uses it.
	SeagullMovement seagullMovement;

	// this should be made into a list for all endpoints.
	[SerializeField] Transform endFlightOne;
	[SerializeField] Transform endFlightTwo;
	[SerializeField] Transform endFlightTree;

	// this should be made into a list for all spawnpoints.
	[SerializeField] Transform seagullSpawnPointsOne;
	[SerializeField] Transform seagullSpawnPointsTwo;
	[SerializeField] Transform seagullSpawnPointsTree;

	// do we need this here or can it be inside the method that spawns it?
	int randomSpawnPoint;

	[SerializeField] SeagullMovement seagullPrefab;

	int currentNumberOfSeagulls = 0;
	int maxNumberOfSeagulls = 1;

	[SerializeField] float spawnIntervalls = 5f;

	private void OnEnable()
	{
		// starts a corutine of spawning our seagulls.
		StartCoroutine("SpawnSeagull");
	}

	private void Start()
	{
		if(GameManager.Settings.SeagullsDontAttack)
		{
			enabled = false;
			StopAllCoroutines();
		}
	}


	// once we remove the corutine we can remove this entire method.
	private void OnDisable()
	{
		// stops all corutines to make sure they don't run in the background
		StopAllCoroutines();
	}

	// this will be what we subscribe to the event in seagull.
	public void Despawn(GameObject seagull)
	{
		// despawns and destroys the seagull 
		// change this to not destroy the seagull. 
		// make it instead work with our object pool.
		Destroy(seagull);
		currentNumberOfSeagulls--;
	}

	// this entire corutine should be removed and made into a script that handles the spawning.
	// make a method in here that is the spawn method, and have it subscribe to an event that 
	// is handled in our spawntimer script.

	// the script should be a timer and update normaly, once the timer is reached for spawninterval
	// that we get from our GameManager Settings we should invoke a event. 

	// this script (SeagullManager) should subscribe to that event with a method
	// that instantiates a new seagull
	IEnumerator SpawnSeagull()
	{
		while(true)
		{       
			// checks if the currentNumberOfSeagulls we have is less then our maximum.
			if (currentNumberOfSeagulls < maxNumberOfSeagulls)
			{
				yield return new WaitForSeconds(spawnIntervalls);
				// gets a random spawnpoint.
				randomSpawnPoint = Random.Range(0, 3);

				// sets our randomSpawnPoint to one of them.
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
				// sets the different points for the seagull to shit on i think
				// this will be needed to be changed
				seagullMovement.BreadPackage = breadPackage;
				seagullMovement.HamPackage = hamPackage;
				seagullMovement.CheesePackage = cheesePackage;

				// increases the number of seagulls.
				currentNumberOfSeagulls++;

				// initializes the seagull.
				seagullMovement.Init(this);
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

    // this will be removed as we give all endpoints to the Seagull itself and will be 
    // using a method in SeagullMovement to get a random endpoint
    void RandomEndPoint()
    {
        // gets a random endPoint for the seagull.
        randomSpawnPoint = Random.Range(0, 3); // rename this to be better named.

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
