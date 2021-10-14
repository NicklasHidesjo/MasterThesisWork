using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Butter : MonoBehaviour
{
	float butteringDone;
	[SerializeField] List<float> butterStageInitiation;

	[SerializeField] GameObject[] butterObjects;

    //[SerializeField] Mesh[] butterStages;

	//[SerializeField] Vector3[] butterStagePositions;

    //MeshFilter meshFilter;
	//[SerializeField] MeshCollider meshCollider;

    [SerializeField] ButterVelocity knife;


    void Start()
    {
        //meshFilter = GetComponent<MeshFilter>();
    }

	private void Update()
	{
		if(knife == null) { return; }
		
		butteringDone += knife.Velocity;

		if (butteringDone > butterStageInitiation[1] && !butterObjects[2].activeSelf)
		{
			/*			
				meshCollider.sharedMesh = butterStages[1];
				meshFilter.mesh = butterStages[1];
				transform.localPosition = butterStagePositions[1];
			*/

			butterObjects[0].SetActive(false);
			butterObjects[1].SetActive(true);
			butterObjects[2].SetActive(false);

		}
		else if(butteringDone > butterStageInitiation[0] && !butterObjects[1].activeSelf)
		{
			/*			
			 	meshCollider.sharedMesh = butterStages[0];
				meshFilter.mesh = butterStages[0];
				transform.localPosition = butterStagePositions[2];
			*/

			butterObjects[0].SetActive(false);
			butterObjects[1].SetActive(false);
			butterObjects[2].SetActive(true);
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.GetComponent<ButterBlade>())
		{
			knife = other.GetComponentInChildren<ButterVelocity>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject.GetComponent<ButterBlade>())
		{
			knife = null;
		}
	}
}
