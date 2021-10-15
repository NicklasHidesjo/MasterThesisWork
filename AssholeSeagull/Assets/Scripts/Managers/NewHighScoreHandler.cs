using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreHandler : MonoBehaviour
{
    [SerializeField] GameObject newRecordObject;
    [SerializeField] AudioSource celebrationSoundPlayer;
    [SerializeField] AudioClip celebrationSound;
    [SerializeField] GameObject CelebrationCanvasObject;

    // maybe the sound related things here should be set into a audio script that
    // handles all audio everywhere.

    public void NewHighScoreCelebration()
    {
        // activate the newRecord object
        newRecordObject.SetActive(true);
        // activate the Celebration canvas
        CelebrationCanvasObject.SetActive(true);

        // set our celebrationSoundPlayers clip to our celebration sound
        celebrationSoundPlayer.clip = celebrationSound;
        // play the clip
        celebrationSoundPlayer.Play();
    }
}
