using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHighScoreHandler : MonoBehaviour
{
    [SerializeField] private GameObject newRecordObject;
    [SerializeField] private AudioSource celebrationSoundPlayer;
    [SerializeField] private AudioClip celebrationSound;
    [SerializeField] private GameObject CelebrationCanvasObject;

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
