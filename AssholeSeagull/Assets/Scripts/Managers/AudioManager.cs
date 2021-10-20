using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Valve.VR.Extras;

public class AudioManager : MonoBehaviour
{
    // look into the possibility to make the sliders be slidable using the pointers 
    // not only button presses. (also look into making it possible to just 
    // press anywhere on the slider and have the volume jump to it.
    // in other words rework the volume sliders.

    [SerializeField] private SteamVR_LaserPointer rightHand;
    [SerializeField] private SteamVR_LaserPointer leftHand;

    [SerializeField] private Slider masterSlider = null;
    [SerializeField] private Slider musicSlider = null;
    [SerializeField] private Slider effectsSlider = null;

    [SerializeField] private AudioMixer mixer;

    [Tooltip("The change that soundvolumes have when pushing either + or - buttons.")]
    [SerializeField] private float soundIncrement;

    private AudioSource buttonPlayer;

    private float masterVolume;
    private float musicVolume;
    private float effectsVolume;

    private float lowestSound = 0.0000001f;

    private void Start()
	{
		SubscribeToEvents();

		GetReferences();

		LoadVolume();
	}

	private void SubscribeToEvents()
	{
		rightHand.PointerClick += PointerClick;
		leftHand.PointerClick += PointerClick;
	}
    private void GetReferences()
    {
        buttonPlayer = GetComponent<AudioSource>();
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Decrease Master")
        {
            Debug.Log("sound -");
            masterVolume -= soundIncrement;
            masterVolume = Mathf.Clamp(masterVolume, lowestSound, 1f);
            SaveVolume();
        }
        if (e.target.name == "Increase Master")
        {
            Debug.Log("sound +");
            masterVolume += soundIncrement;
            masterVolume = Mathf.Clamp(masterVolume, lowestSound, 1f);
            SaveVolume();
        }

        if (e.target.name == "Decrease Music")
        {
            musicVolume -= soundIncrement;
            musicVolume = Mathf.Clamp(musicVolume, lowestSound, 1f);
            SaveVolume();
        }
        if (e.target.name == "Increase Music")
		{
            musicVolume += soundIncrement;
            musicVolume = Mathf.Clamp(musicVolume, lowestSound, 1f);
            SaveVolume();
        }

        if (e.target.name == "Decrease Effects")
        {
            effectsVolume -= soundIncrement;
            effectsVolume = Mathf.Clamp(effectsVolume, lowestSound, 1f);
            SaveVolume();
        }
        if (e.target.name == "Increase Effects")
		{
            effectsVolume += soundIncrement;
            effectsVolume = Mathf.Clamp(effectsVolume, lowestSound, 1f);
            SaveVolume();
        }
    }

    private void LoadVolume()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);

        masterSlider.value = masterVolume;
        SetVolume("Master", masterVolume);
        musicSlider.value = musicVolume;
        SetVolume("Music", musicVolume);
        effectsSlider.value = effectsVolume;
        SetVolume("Effects", effectsVolume);
    }
    private void SaveVolume()
    {
        buttonPlayer.Play();

        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        masterSlider.value = masterVolume;
        SetVolume("Master", masterVolume);
        musicSlider.value = musicVolume;
        SetVolume("Music", musicVolume);
        effectsSlider.value = effectsVolume;
        SetVolume("Effects", effectsVolume);
    }

    public void SetVolume(string volume , float value)
	{
		mixer.SetFloat(volume, Mathf.Log10(value) * 20);
	}

    private void OnDestroy()
	{
		UnsubscribeFromEvents();
	}
	private void UnsubscribeFromEvents()
	{
		rightHand.PointerClick -= PointerClick;
		leftHand.PointerClick -= PointerClick;
	}
}
