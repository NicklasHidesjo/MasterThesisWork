using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomBun : MonoBehaviour
{
    [SerializeField] LayerMask layer;

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, Mathf.Infinity, layer.value ))
        {
            InteractableItem item = hit.transform.gameObject.GetComponentInChildren<InteractableItem>();

            if (item.InHand) { return; }
            if (item.Velocity.sqrMagnitude > 0.01) { return; }

            Debug.Log("Sandwich finished");
        }
    }
}
