using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullManager : MonoBehaviour
{
	[SerializeField] private List<Transform> poopTargets = new List<Transform>();
	[SerializeField] private List<Transform> endFlightTransforms = new List<Transform>();
	[SerializeField] private List<Transform> startFlightTransforms = new List<Transform>();
	[SerializeField] private List<SeagullSettings> seagullSettings = new List<SeagullSettings>();

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
		GetAllPoopTargets();

		spawnInterval = GameManager.Settings.SeagullSpawnInterval;
		timer = Random.Range(spawnInterval.x, spawnInterval.y);
		CreateSeagullPool();
	}

	private void CreateSeagullPool()
	{
		int maxNumberOfSeagulls = GameManager.Settings.SeagullAmount;
		for (int i = 0; i < maxNumberOfSeagulls; i++)
		{
			SeagullController newSeagull = Instantiate(seagullPrefab, seagullParent);
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
		GetAllPoopTargets();
        // gets a random spawnpoint.
        Transform spawnPoint = GetTransformFromList(startFlightTransforms);
        Transform endPoint = GetTransformFromList(endFlightTransforms);
		Transform foodPackage = GetTransformFromList(poopTargets);

		int random = Random.Range(0, seagullSettings.Count);
		SeagullSettings seagullSetting = seagullSettings[random];
		seagull.SeagullSettings = seagullSetting;

		seagull.FoodTarget = foodTracker.GetRandomTarget();
        seagull.FlightEnd = endPoint.position;
		seagull.FoodPackage = foodPackage.position;
		seagull.transform.position = spawnPoint.position;

		seagull.gameObject.SetActive(true);
		seagull.GetComponent<StateMachine>().ChangeState(States.Idle);
    }

	private void GetAllPoopTargets()
	{
		poopTargets.Clear();

		Tomato[] tomatoes = FindObjectsOfType<Tomato>();
		LettuceHead[] lettuceHeads = FindObjectsOfType<LettuceHead>();
		FoodPackage[] foodPackages = FindObjectsOfType<FoodPackage>();

		foreach (var tomato in tomatoes)
		{
			poopTargets.Add(tomato.transform);
		}
		foreach (var lettuce in lettuceHeads)
		{
			poopTargets.Add(lettuce.transform);
		}
		foreach (var foodPackage in foodPackages)
		{
			poopTargets.Add(foodPackage.transform);
		}
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
