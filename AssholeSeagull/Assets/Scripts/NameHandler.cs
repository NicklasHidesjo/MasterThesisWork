using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.Extras;

public class NameHandler : MonoBehaviour
{
    [Header("inappropriate names")]
    [SerializeField] private List<string> inappropriateNames = new List<string>();
    [SerializeField] private string punishment;
    [Header("Other stuff")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI nameText2;
    [SerializeField] private GameObject gameButtons;

    [SerializeField] private SteamVR_LaserPointer rightHand;
    [SerializeField] private SteamVR_LaserPointer leftHand;

    [SerializeField] private int maxCharacters;

    private string playerName = "";

    void OnEnable()
    {
        rightHand.PointerClick += PointerClick;
        leftHand.PointerClick += PointerClick;
        nameText.text = GameManager.Name;
    }

    private void PointerClick(object sender, PointerEventArgs e)
    {
        if(e.target.name == "Enter")
        {
            if(InappropriateName(playerName))
            {
                playerName = punishment;
            }

            GameManager.Name = playerName;
            nameText2.text = playerName;
            gameButtons.SetActive(true);
            gameObject.SetActive(false);
            return;
        }
        if(e.target.name == "Clear")
        {
            playerName = "";

            SetNameText();
            return;
        }
        if(e.target.name == "Delete")
        {
            char[] characters = playerName.ToCharArray();
            playerName = "";

            for (int i = 0; i < characters.Length - 1; i++)
            {
                playerName += characters[i];
            }
           

            SetNameText();
            return;
        }

        if(playerName.ToCharArray().Length >= maxCharacters)
        {
            return;
        }

        if(e.target.name == "Space")
        {
            playerName += " ";
        }
        else
        {
            playerName += e.target.name;
        }

        SetNameText();
    }

    private bool InappropriateName(string possibleName)
    {
        // check so that no inappropriate words are hidden
        // in other names.

        char[] characters = possibleName.ToCharArray();
        possibleName = "";

        foreach (var character in characters)
        {
            if (character == ' ')
            {
                continue;
            }
            possibleName += character;
        }

        foreach (var name in inappropriateNames)
        {
            if(possibleName == name.ToUpper())
            {
                return true;
            }
        }

        return false;
    }

    private void SetNameText()
    {
        nameText.text = playerName;
    }

    private void OnDisable()
    {
        rightHand.PointerClick -= PointerClick;
        leftHand.PointerClick -= PointerClick;
    }
}