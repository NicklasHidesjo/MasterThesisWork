using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR.Extras;

public class NameHandler : MonoBehaviour
{
    public static event RotateBoards RotateBoards;

    [Header("inappropriate names")]
    [SerializeField] private List<string> inappropriateNames = new List<string>();
    [SerializeField] private string punishment;
    [Header("Other stuff")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI nameText2;

    [SerializeField] private SteamVR_LaserPointer rightHand;
    [SerializeField] private SteamVR_LaserPointer leftHand;

    [SerializeField] private int maxCharacters;

    private CorkBoardController corkBoardController;

    private string playerName = "";

    private void Awake()
    {
        corkBoardController = FindObjectOfType<CorkBoardController>();
    }

    void OnEnable()
    {
        rightHand.PointerClick += PointerClick;
        leftHand.PointerClick += PointerClick;
        nameText.text = GameManager.Name;
    }

    private void PointerClick(object sender, PointerEventArgs e)
    {
        if(corkBoardController.Rotating)
        {
            return;
        }
        HandleChangingName(e.target.name);
    }

    public void HandleChangingName(string input)
    {
        if (input == "Enter")
        {
            SetPlayerName();
            return;
        }
        if (input == "Clear")
        {
            EmptyPlayerName();
            return;
        }
        if (input == "Delete" || input == "BackSpace")
        {
            RemoveLastCharacter();
            return;
        }
        AddCharacter(input);
    }

    public void SetPlayerName()
    {
        if (InappropriateName(playerName))
        {
            playerName = punishment;
        }

        GameManager.Name = playerName;
        nameText2.text = playerName;
        RotateBoards?.Invoke("Back");
    }
    
    public void EmptyPlayerName()
    {
        playerName = "";

        SetNameText();
    }

    public void RemoveLastCharacter()
    {
        char[] characters = playerName.ToCharArray();
        playerName = "";

        for (int i = 0; i < characters.Length - 1; i++)
        {
            playerName += characters[i];
        }


        SetNameText();
    }

    public void AddCharacter(string character)
    {
        if(character.Length >1)
        {
            return;
        }

        if (playerName.ToCharArray().Length >= maxCharacters)
        {
            return;
        }

        if (character == "Space" || character == "Dash")
        {
            playerName += " ";
        }
        else
        {
            playerName += character;
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
