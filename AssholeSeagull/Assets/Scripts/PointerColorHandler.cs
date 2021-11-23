using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.Extras;

public class PointerColorHandler : MonoBehaviour
{
    [SerializeField] private Color nonInteractiveColor;
    [SerializeField] private Color interactiveColor;
    [SerializeField] private Color clickColor;

    [SerializeField] private float interactiveThickness;
    [SerializeField] private float nonInteractiveThickness;

    [SerializeField] private string interactiveTag = "UIButton";
    
    private SteamVR_LaserPointer hand;

    private void Awake()
    {
        hand = GetComponent<SteamVR_LaserPointer>();

        hand.clickColor = clickColor;

        hand.PointerIn += PointerBeginHover;
        hand.PointerOut += PointerEndHover;
    }


    private void PointerEndHover(object sender, PointerEventArgs e)
    {
        hand.thickness = nonInteractiveThickness;
        hand.color = nonInteractiveColor;
    }

    private void PointerBeginHover(object sender, PointerEventArgs e)
    {
        if(e.target.CompareTag(interactiveTag))
        {
            hand.thickness = interactiveThickness;
            hand.color = interactiveColor;
        }
    }
}
