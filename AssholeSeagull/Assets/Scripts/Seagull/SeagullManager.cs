using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullManager : MonoBehaviour
{
	// this script will be rewritten and changed, to a great extent

	[SerializeField] private List<Transform> foodPackages = new List<Transform>();
	[SerializeField] private List<Transform> endFlightTransforms = new List<Transform>();
	[SerializeField] private List<Transform> startFlightTransforms = new List<Transform>();

	[SerializeField] SeagullMovement seagullPrefab;

	[SerializeField] private Transform seagullParent;

	private List<SeagullMovement> seagullPool = new List<SeagullMovement>();
	
	private float spawnIntervalls = 5f;
	private float timer = 0;

	private void Start()
	{
		if (GameManager.Settings.SeagullsDontAttack)
		{
			enabled = false;
			StopAllCoroutines();
		}
		spawnIntervalls = GameManager.Settings.SeagullSpawnInterval;

		CreateSeagullPool();
	}

	private void CreateSeagullPool()
	{
		int maxNumberOfSeagulls = GameManager.Settings.SeagullAmount;
		for (int i = 0; i < maxNumberOfSeagulls; i++)
		{
			SeagullMovement newSeagull = Instantiate(seagullPrefab, seagullParent);
			newSeagull.FoodPackages = foodPackages;
			seagullPool.Add(newSeagull);
		}

		foreach (var seagull in seagullPool)
		{
			seagull.gameObject.SetActive(false);
		}
	}

	// turn update into its own timer script?
    private void Update()
    {
		timer += Time.deltaTime;
		if (timer > spawnIntervalls)
		{
			SeagullMovement seagull = GetInactiveSeagull();
			if (seagull != null)
			{
				timer = 0;
				SpawnSeagull(seagull);
			}
		}
	}

	// this will be what we subscribe to the event in seagull.
	public void Despawn(GameObject seagull)
	{
		seagull.SetActive(false);
	}

	// this script (SeagullManager) should subscribe to that event with a method
	// that instantiates a new seagul (will probably not do this)
	private void SpawnSeagull(SeagullMovement seagull)
    {
        // gets a random spawnpoint.
        Transform spawnPoint = GetTransformFromList(startFlightTransforms);
        Transform endPoint = GetTransformFromList(endFlightTransforms);

        seagull.flightEnd = endPoint;
		seagull.transform.position = spawnPoint.position;

		seagull.gameObject.SetActive(true);

        // initializes the seagull.
        seagull.Init(this);
    }

    private SeagullMovement GetInactiveSeagull()
    {
        foreach (var seagull in seagullPool)
        {
            if (seagull.gameObject.activeSelf)
            {
                continue;
            }

			return seagull;
        }

		return null;
    }
    private Transform GetTransformFromList(List<Transform> transforms)
	{
		int index = Random.Range(0, transforms.Count);
		return transforms[index];
	}
}
