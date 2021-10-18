using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] private Animator poopAnimator;

    private void OntriggerEnter(Collider other)
    { 
        // as this destroys itself we might not need anything but the destroy line 
        // the animation won't play as we destroy the object immediately after.
        Debug.Log("poop landed");
        poopAnimator.SetTrigger("Splatt");

        Destroy(gameObject);
    }
}
