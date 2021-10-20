using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer: MonoBehaviour
{
/*    [SerializeField] AudioSource audioSource;
    
	
	AudioSource audioSource;
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public static void PlaySound(AudioSource source, AudioClip clip)
	{
		source.clip = clip;
		source.Play();
	}
*/


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
