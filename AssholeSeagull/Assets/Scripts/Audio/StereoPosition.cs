using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StereoPosition : MonoBehaviour
{
	[SerializeField] BackgroundMusicPlayer bgPlayer;
	void Start()
	{
		BackgroundMusicPlayer player = FindObjectOfType<BackgroundMusicPlayer>();

		if(player == null)
		{
			player = Instantiate(bgPlayer);
		}

		player.transform.position = transform.position;
		player.transform.rotation = transform.rotation;
	}

	void Update()
	{
		
	}
}
