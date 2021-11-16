using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
	[SerializeField] private List<Transform> endPoint;
    [SerializeField] private float maxDistance;

	private int index = 0;
	private NavMeshAgent agent;

	Animator animator;	

    void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		SetDestination();
		IncreaseIndex();
	}

	private void Update()
	{
		animator.SetFloat("Speed", agent.speed);

		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				IncreaseIndex();
				SetDestination();
			}
		}
	}

	private void SetDestination()
	{
		Vector3 newDestination = GetRandomPoint(transform.position, maxDistance);
		//agent.destination = endPoint[index].position;

        if (newDestination == Vector3.zero)
        {
			SetDestination();
        }
        else
        {
			agent.destination = newDestination;
        }
	}

	public static Vector3 GetRandomPoint(Vector3 center, float maxDistance)
	{
		// Get Random Point inside Sphere which position is center, radius is maxDistance
		Vector3 randomPos = Random.insideUnitSphere * maxDistance + center;

		NavMeshHit hit; // NavMesh Sampling Info Container

        // from randomPos find a nearest point on NavMesh surface in range of maxDistance
        if (NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas))
        {
		return hit.position;
        }
		return Vector3.zero;
	}

	private void IncreaseIndex()
	{
		index++;

		if (index >= endPoint.Count)
		{
			index = 0;
		}
	}
}
