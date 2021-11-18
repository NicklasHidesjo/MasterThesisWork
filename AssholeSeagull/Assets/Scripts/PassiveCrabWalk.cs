using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassiveCrabWalk : MonoBehaviour
{
	[SerializeField] private Transform centerPoint;
	[SerializeField] private float maxDistance;

	private NavMeshAgent agent;

	Animator animator;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		SetDestination();
	}

	private void Update()
	{
		animator.SetFloat("Speed", agent.speed);

		if (!agent.pathPending)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				SetDestination();
			}
		}
	}

	private void SetDestination()
	{
		Vector3 newDestination = GetRandomPoint();
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

	private Vector3 GetRandomPoint()
	{
		// Get Random Point inside Sphere which position is center, radius is maxDistance
		Vector3 randomPos = Random.insideUnitSphere * maxDistance + centerPoint.position;

		NavMeshHit hit; // NavMesh Sampling Info Container

		// from randomPos find a nearest point on NavMesh surface in range of maxDistance
		if (NavMesh.SamplePosition(randomPos, out hit, maxDistance, NavMesh.AllAreas))
		{
			return hit.position;
		}
		return Vector3.zero;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(centerPoint.position, maxDistance);
	}
}
