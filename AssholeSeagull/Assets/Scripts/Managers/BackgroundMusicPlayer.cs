using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] AudioClip[] tracks;
    AudioSource audioSource;

    int trackIndex;

    private void Awake()
    {
        // gets all the BackgroundMusicPlayers in the current scene.
        BackgroundMusicPlayer[] backgroundMusicPlayers = FindObjectsOfType<BackgroundMusicPlayer>();
       
        // checks if there is more then 1 BackgroundMusicPlayer.
        if(backgroundMusicPlayers.Length > 1)
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

    void Start()
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

    void Update()
    {
        // checks if we have audio still playing.
        if(!audioSource.isPlaying)
        {
            // plays the next track in our list of tracks
            PlayNextTrack();
            // increases the trackIndex.
            IncreaseAudioIndex();
        }
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
