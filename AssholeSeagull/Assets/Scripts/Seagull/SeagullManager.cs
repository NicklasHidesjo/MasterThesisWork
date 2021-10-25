using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullManager : MonoBehaviour
{
	// this script will be rewritten and changed, to a great extent

	//Food Packages
	[SerializeField] Transform breadPackage;
	[SerializeField] Transform cheesePackage;
	[SerializeField] Transform hamPackage;

	SeagullMovement seagullMovement;

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

	private GameManager gameManager;
	private void OnEnable()
	{
		// starts a corutine of spawning our seagulls.
		StartCoroutine("SpawnSeagull");
	}

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		
		if(gameManager.Settings.SeagullsDontAttack)
		{
			enabled = false;
			StopAllCoroutines();
		}
	}


	private void OnDisable()
	{
		// stops all corutines to make sure they don't run in the background
		StopAllCoroutines();
	}

	public void Despawn(GameObject seagull)
	{
		// despawns and destroys the seagull 
		Destroy(seagull);
		currentNumberOfSeagulls--;
	}

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
