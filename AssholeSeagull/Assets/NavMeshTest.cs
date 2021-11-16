using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
	[SerializeField] private List<Transform> endPoint;
	private int index = 0;
	private NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		SetDestination();
		IncreaseIndex();
	}

	private void Update()
	{
		Debug.Log("destination: " + agent.destination);
		Debug.Log("position: " + transform.position);

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
		agent.destination = endPoint[index].position;
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
