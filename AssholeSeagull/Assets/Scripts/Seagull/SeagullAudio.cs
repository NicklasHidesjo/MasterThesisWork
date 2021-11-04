using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullAudio : MonoBehaviour
{
    [SerializeField] private AudioClip spawnSound;
    [SerializeField] private AudioClip poopingSound;
    [SerializeField] private AudioClip scaredSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySpawnSound()
    {
        audioSource.clip = spawnSound;
        audioSource.Play();
    }

    public void PlayPoopingSound()
    {
        audioSource.clip = poopingSound;
        audioSource.Play();
    }

    public void PlayScaredSound()
    {
        audioSource.clip = scaredSound;
        audioSource.Play();
    }
}
