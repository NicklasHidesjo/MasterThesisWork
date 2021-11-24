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

    private bool inPauseMenu;

    private void Start()
    {
        if (pose == null)
            pose = GetComponent<SteamVR_Behaviour_Pose>();
        if (pose == null)
            Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

        pauseMenu.SetActive(false);

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

        InGameVoiceRec.pause += TogglePaus;
        InGameVoiceRec.Recenter += Recenter;
        InGameVoiceRec.Restart += Restart;
        InGameVoiceRec.MainMenu += GoToMainMenu;
        InGameVoiceRec.Quit += Quit;


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
        rightHand.pointer.SetActive(true);
        leftHand.pointer.SetActive(true);

        inPauseMenu = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }
    private void ResumeGame()
    {
        leftHand.pointer.SetActive(false);
        rightHand.pointer.SetActive(false);

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
        //ResumeGame();
    }

    private void Restart()
    {
        if (!inPauseMenu)
        {
            return;
        }
        Time.timeScale = 1;
        PlayButtonSound();
        StartCoroutine(SceneLoader.LoadSceneAsync(SceneLoader.GetSceneName()));
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
        StartCoroutine(SceneLoader.LoadSceneAsync("MainMenu"));
    }


    private void PlayButtonSound()
    {
        buttonPlayer.Play();
    }


    private void OnDisable()
    {
        Time.timeScale = 1;
        UnsubscribeFromEvents();
    }

    private void UnsubscribeFromEvents()
    {
        rightHand.PointerClick -= PointerClick;
        leftHand.PointerClick -= PointerClick;

        InGameVoiceRec.pause -= TogglePaus;
        InGameVoiceRec.Recenter -= Recenter;
        InGameVoiceRec.Restart -= Restart;
        InGameVoiceRec.MainMenu -= GoToMainMenu;
        InGameVoiceRec.Quit -= Quit;
    }
}
