using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update 

    [SerializeField] AudioClip[] clips;
    AudioSource audioSource;

    int audioIndex;

    private void Awake()
    {
        BackgroundMusicPlayer[] backgroundMusicPlayers = FindObjectsOfType<BackgroundMusicPlayer>();
        if(backgroundMusicPlayers.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        audioIndex = 0;
        // make random? 
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
        IncreaseAudioIndex();
    }

    private void PlayNextTrack()
    {
        audioSource.clip = clips[audioIndex];
        audioSource.Play();
    }

    private void IncreaseAudioIndex()
    {
        audioIndex++;
        if(audioIndex >= clips.Length)
        {
            audioIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayNextTrack();
            IncreaseAudioIndex();
        }
    }
}
