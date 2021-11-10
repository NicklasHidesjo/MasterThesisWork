using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoCutZone : MonoBehaviour
{
    [SerializeField] private bool topCutZone;

    private Tomato tomato;

    void Start()
    {
        tomato = GetComponentInParent<Tomato>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " got triggered");
        if (other.CompareTag("KnifeBlade"))
        {
            if (topCutZone)
            {
                tomato.SlicingTomato(false, other.gameObject);
            }
            else
            {
                tomato.SlicingTomato(true, other.gameObject);
            }
        }
    }
}
