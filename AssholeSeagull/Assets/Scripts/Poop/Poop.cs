using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField] Animator poopAnimator;

    private void OntriggerEnter(Collider other)
    {
        Debug.Log("poop landed");
        poopAnimator.SetTrigger("Splatt");

        Destroy(gameObject);
    }
}
