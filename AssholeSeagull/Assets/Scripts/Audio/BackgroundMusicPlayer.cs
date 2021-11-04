using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks;
    [SerializeField] private int shuffleAmount = 10;

    private AudioSource audioSource;

    private int trackIndex = 0;

    private void Awake()
	{
		CheckForOtherPlayers();
        ShuffleTracks();
        // swap places between the two tracks
	}

    private void ShuffleTracks()
    {
        for (int i = 0; i < shuffleAmount; i++)
        {
            int index = Random.Range(0, tracks.Length);
            int newIndex = Random.Range(0, tracks.Length);

            AudioClip tmp = tracks[newIndex];
            tracks[newIndex] = tracks[index];
            tracks[index] = tmp;
        }
    }

    private void CheckForOtherPlayers()
	{
		// gets all the BackgroundMusicPlayers in the current scene.
		BackgroundMusicPlayer[] backgroundMusicPlayers = FindObjectsOfType<BackgroundMusicPlayer>();

		// checks if there is more then 1 BackgroundMusicPlayer.
		if (backgroundMusicPlayers.Length > 1)
		{
			// destroys itself if more then one is found
			Destroy(gameObject);
		}
		else
		{
			// if it's alone it sets itself to DontDestroyOnLoad.
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
    {
        // sets our trackindex to be zero.
        trackIndex = 0; // make random? 

        // gets the AudioSource on our object.
        audioSource = GetComponent<AudioSource>();

        // plays the next track in our list.
        PlayNextTrack();
        // increases trackIndex
        IncreaseAudioIndex();
    }
    
    private void FixedUpdate()
    {
        // checks if we have audio still playing.
        if (!audioSource.isPlaying)
        {
            ChangeTrack();
        }
    }

    private void ChangeTrack()
    {
        // plays the next track in our list of tracks
        PlayNextTrack();
        IncreaseAudioIndex();
    }

    private void PlayNextTrack()
    {
        // change the clip in our audioSource
        audioSource.clip = tracks[trackIndex];

        // play the audioSource clip.
        audioSource.Play();
    }
    private void IncreaseAudioIndex()
    {
        // increase our trackIndex.
        trackIndex++;
        // check if our index is greater then or equall to,
        // the lengh of our array with tracks.
        if(trackIndex >= tracks.Length)
        {
            // sets our index to 0 thus looping it all.
            trackIndex = 0;
        }
    }
}
