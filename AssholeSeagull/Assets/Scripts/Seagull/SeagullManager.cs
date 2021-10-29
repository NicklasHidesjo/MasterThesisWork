using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullManager : MonoBehaviour
{
	[SerializeField] private List<Transform> foodPackages = new List<Transform>();
	[SerializeField] private List<Transform> endFlightTransforms = new List<Transform>();
	[SerializeField] private List<Transform> startFlightTransforms = new List<Transform>();

	[SerializeField] SeagullController seagullPrefab;

	[SerializeField] private Transform seagullParent;

	private List<SeagullController> seagullPool = new List<SeagullController>();
	
	private Vector2 spawnInterval;

	private float timer = 0;

	private FoodTracker foodTracker;

	private void Start()
	{
		if (GameManager.Settings.SeagullsDontAttack)
		{
			enabled = false;
			StopAllCoroutines();
		}
		foodTracker = FindObjectOfType<FoodTracker>();

		spawnInterval = GameManager.Settings.SeagullSpawnInterval;
		timer = Random.Range(spawnInterval.x, spawnInterval.y);
		Debug.Log(timer);
		CreateSeagullPool();
	}

	private void CreateSeagullPool()
	{
		int maxNumberOfSeagulls = GameManager.Settings.SeagullAmount;
		for (int i = 0; i < maxNumberOfSeagulls; i++)
		{
			SeagullController newSeagull = Instantiate(seagullPrefab, seagullParent);
			newSeagull.Init();
			seagullPool.Add(newSeagull);
		}

		foreach (var seagull in seagullPool)
		{
			seagull.gameObject.SetActive(false);
		}
	}

    private void Update()
    {
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			SeagullController seagull = GetInactiveSeagull();
			if (seagull != null)
			{
				timer = Random.Range(spawnInterval.x, spawnInterval.y);
				SpawnSeagull(seagull);
			}
		}
	}

	private void SpawnSeagull(SeagullController seagull)
    {
        // gets a random spawnpoint.
        Transform spawnPoint = GetTransformFromList(startFlightTransforms);
        Transform endPoint = GetTransformFromList(endFlightTransforms);
		Transform foodPackage = GetTransformFromList(foodPackages);

		seagull.FoodTarget = foodTracker.GetRandomTarget();
        seagull.FlightEnd = endPoint.position;
		seagull.FoodPackage = foodPackage.position;
		seagull.transform.position = spawnPoint.position;
		seagull.ResetBird();

		seagull.gameObject.SetActive(true);
		seagull.GetComponent<StateMachine>().ChangeState(States.Idle);
    }

    private SeagullController GetInactiveSeagull()
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
