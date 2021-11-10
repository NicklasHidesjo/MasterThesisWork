using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DonePooping(SeagullController seagullController);

public class Pooping : MonoBehaviour
{
    public static event DonePooping Pooped;

    // move this into Seagull controller
    [SerializeField] private GameObject poopPrefab;
    [SerializeField] private Transform spawnPosition;
    private SeagullAudio seagullAudio;
    private SeagullController seagull;

    private void Start()
    {
        seagull = GetComponent<SeagullController>();
        seagullAudio = GetComponent<SeagullAudio>();
    }

    public void Poop()
    {
        // creates a new poop.
        Instantiate(poopPrefab, spawnPosition.transform.position, Quaternion.identity);
        seagullAudio.PlayPoopingSound();
    }

    public void DonePooping()
	{
        Pooped?.Invoke(seagull);
	}
}
