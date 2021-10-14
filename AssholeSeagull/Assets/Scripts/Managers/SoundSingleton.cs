using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSingleton : MonoBehaviour
{
    [SerializeField] AudioSource seagullAudio;
    [SerializeField] AudioSource poopAudio;

    public void SeagullFx(AudioClip clip)
    {
        seagullAudio.clip = clip;
        seagullAudio.Play();
    }

    public void Seagull(AudioClip clip)
    {
        seagullAudio.clip = clip;
        seagullAudio.Play();
    }

    public void PoopOnFood(AudioClip clip)
    {
        poopAudio.clip = clip;
        poopAudio.Play();
    }
}
