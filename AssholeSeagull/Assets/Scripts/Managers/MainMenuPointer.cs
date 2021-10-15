                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class MainMenuPointer : MonoBehaviour // rename to something better
{
    // this script uses events from SteamVR_LaserPointer 
    // to see if either the right or left hand clicks any
    // buttons. 

    // look into removing the if else if statement and just have a bunch of if statements

    // look into the posibility for it to not require being buttons in the canvas
    // but just objects with colliders.


    [SerializeField] SteamVR_LaserPointer rightHand;
    [SerializeField] SteamVR_LaserPointer leftHand;

    SceneLoader sceneLoader;
    AudioSource buttonPlayer;

    private void Start()
    {
        rightHand.PointerClick += PointerClick;
        leftHand.PointerClick += PointerClick;

        buttonPlayer = FindObjectOfType<AudioSource>();

        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Play")
        {
            PlayButtonSound();
            sceneLoader.LoadScene("GameScene");
            Debug.Log("Button was clicked");
        }

        else if(e.target.name == "Replay")
        {
            PlayButtonSound();
            sceneLoader.LoadScene("GameScene");
        }    

        else if (e.target.name == "Quit")
        {
            PlayButtonSound();
            sceneLoader.Quit();
        }

        else if(e.target.name == "Main Menu")
		{
            PlayButtonSound();
            sceneLoader.LoadScene(0);
		}

        else if(e.target.name == "Free Roam")
        {
            PlayButtonSound();
            sceneLoader.LoadScene("FreeRoamMode");
        }
    }

    public void PlayButtonSound()
    {
        buttonPlayer.Play();
    }

    private void OnDestroy()
    {
        rightHand.PointerClick -= PointerClick;
        leftHand.PointerClick -= PointerClick;
    }
}
