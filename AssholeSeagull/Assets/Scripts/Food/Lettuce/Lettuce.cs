using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Lettuce : MonoBehaviour
{
    [SerializeField] LettuceHead head = null;

    MeshCollider meshCollider;

    private Interactable interactable;

    private void Awake()
    {
        interactable = GetComponentInParent<Interactable>();
        meshCollider = GetComponent<MeshCollider>();
    }

    private void GotPickedUp(Hand hand)
    {
        UnsubscribeFromEvents();
    }

    private void OnEnable()
    {
        SubscribeToEvents();
        meshCollider.enabled = false;
    }

    private void SubscribeToEvents()
    {
        interactable.onAttachedToHand += GotPickedUp;

        head = GetComponentInParent<LettuceHead>();
        if (head != null)
        {
            head.PickedUp += PickedUp;
            head.PutDown += PutDown;
        }
    }

    private void PickedUp()
    {
        meshCollider.enabled = true;
    }
    private void PutDown()
    {
        meshCollider.enabled = false;
    }

    private void UnsubscribeFromEvents()
    {
        interactable.onAttachedToHand -= GotPickedUp;
        if (head != null)
        {
            head.PickedUp -= PickedUp;
            head.PutDown -= PutDown;
        }
        head = null;
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
}
