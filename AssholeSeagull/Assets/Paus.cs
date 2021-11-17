using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;

public class Paus : MonoBehaviour
{
    private SteamVR_Action_Boolean pauseInput = SteamVR_Input.GetBooleanAction("Pause");
    private SteamVR_Behaviour_Pose pose;

    [SerializeField] private SteamVR_LaserPointer rightHand;
    [SerializeField] private SteamVR_LaserPointer leftHand;

    private AudioSource buttonPlayer;
    private VRRecenteringController vrRecenteringController;

    [SerializeField] private GameObject pauseMenu;

    private float rayThickness;

    private bool inPauseMenu;

    private void Start()
    {
        if (pose == null)
            pose = GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

        pauseMenu.SetActive(false);
        rayThickness = leftHand.thickness;

        leftHand.thickness = 0;
        rightHand.thickness = 0;

        vrRecenteringController = FindObjectOfType<VRRecenteringController>();
        buttonPlayer = GetComponent<AudioSource>();
        SubscribeToEvents();
    }

    private void Update()
    {
        if (pauseInput.GetStateDown(pose.inputSource))
        {
            Pause();
        }
    }

    private void PointerEndHover(object sender, PointerEventArgs e)
    {
        rightHand.thickness = 0;
        leftHand.thickness = 0;
    }

    private void PointerBeginHover(object sender, PointerEventArgs e)
    {
        if (!inPauseMenu)
        {
            return;
        }
        rightHand.thickness = rayThickness;
        leftHand.thickness = rayThickness;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Resume")
        {
            ResumeGame();

        }
        if (e.target.name == "Recenter")
        {
            Recenter();

        }
        if (e.target.name == "Restart")
        {
            Restart();
        }


        if (e.target.name == "Quit")
        {
            Quit();
        }

        if (e.target.name == "MainMenu")
        {
            GoToMainMenu();
        }
    }

    private void SubscribeToEvents()
    {
        rightHand.PointerClick += PointerClick;
        leftHand.PointerClick += PointerClick;

        rightHand.PointerIn += PointerBeginHover;
        leftHand.PointerIn += PointerBeginHover;

        leftHand.PointerOut += PointerEndHover;
        rightHand.PointerOut += PointerEndHover;


        VoiceRecognition.pause += TogglePaus;
        VoiceRecognition.Recenter += Recenter;
        VoiceRecognition.Restart += Restart;
        VoiceRecognition.MainMenu += GoToMainMenu;
        VoiceRecognition.Quit += Quit;


    }
    private void TogglePaus(bool pause)
    {
        if (pause)
        {
            Pause();
        }
        else
        {
            ResumeGame();
        }
    }

    private void Pause()
    {
        inPauseMenu = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    private void ResumeGame()
    {
        inPauseMenu = false;
        PlayButtonSound();
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void Recenter()
    {
        if (!inPauseMenu)
        {
            return;
        }
        PlayButtonSound();
        vrRecenteringController.Recenter();
        ResumeGame();
    }

    private void Restart()
    {
        if (!inPauseMenu)
        {
            return;
        }
        Time.timeScale = 1;
        PlayButtonSound();
        SceneLoader.ReloadScene();
    }

    private void Quit()
    {
        if (!inPauseMenu)
        {
            return;
        }
        PlayButtonSound();
        SceneLoader.Quit();
    }

    private void GoToMainMenu()
    {
        if (!inPauseMenu)
        {
            return;
        }
        PlayButtonSound();
        SceneLoader.LoadScene("MainMenu");
    }


    private void PlayButtonSound()
    {
        buttonPlayer.Play();
    }


    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void UnsubscribeFromEvents()
    {
        rightHand.PointerClick -= PointerClick;
        leftHand.PointerClick -= PointerClick;

        rightHand.PointerIn -= PointerBeginHover;
        leftHand.PointerIn -= PointerBeginHover;

        leftHand.PointerOut -= PointerEndHover;
        rightHand.PointerOut -= PointerEndHover;

        VoiceRecognition.pause -= TogglePaus;
        VoiceRecognition.Recenter -= Recenter;
        VoiceRecognition.Restart -= Restart;
        VoiceRecognition.MainMenu -= GoToMainMenu;
        VoiceRecognition.Quit -= Quit;
    }
}
