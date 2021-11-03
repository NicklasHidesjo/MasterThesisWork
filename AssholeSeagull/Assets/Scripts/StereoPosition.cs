using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoPosition : MonoBehaviour
{
	void Start()
	{
		FindObjectOfType<BackgroundMusicPlayer>().transform.position = transform.position;
	}

	void Update()
	{
		
	}
}
