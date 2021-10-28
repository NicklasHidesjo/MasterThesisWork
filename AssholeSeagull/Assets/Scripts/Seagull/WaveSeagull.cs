using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSeagull : MonoBehaviour
{
    [Header("Scare Settings")]
    [Tooltip("The layer that the seagulls are at")]
    [SerializeField] private LayerMask seagullLayer;
    [Tooltip("The radius in unity units that you can scare the bird from")]
    [SerializeField] private float scareRadius;
    [Tooltip("The minimum velocity the players hands need to have to scare the bird")]
    [SerializeField] private float scareVelocityThreshold;

    [Header("VR-Tracking")]
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform rightHandTransform;
    [SerializeField] private Transform leftHandTransform;

    [Header("Sound Settings")]
    [SerializeField] private AudioClip[] shooSounds;
    [SerializeField] private AudioSource shooPlayer;
    // a tracker to keep the same shooSound to be played twice.
    private int lastSoundIndex = -1;

    // the old positions of the left and right hand (used when tracking velocity)
    private Vector3 oldLeftPosition = Vector3.zero;
    private Vector3 oldRightPosition = Vector3.zero;

    // the current positions of the VR-equipment
    private Vector3 currentHeadPos = Vector3.zero;
    private Vector3 currentRightHandPos = Vector3.zero;
    private Vector3 currentLeftHandPos = Vector3.zero;

    private float timePassed = 0; // remove this and make it so that scaring wont get triggered at start

    private void Start()
    {
        // set the old positions of the left and right hand.
        SetOldHandPos();
    }

    private void SetOldHandPos()
    {
        oldLeftPosition = leftHandTransform.position;
        oldLeftPosition.y = 0;
        oldRightPosition = rightHandTransform.position;
        oldRightPosition.y = 0;
    }

    private void Update()
    {
        // this should be removed once we find and fix the bug where you trigger
        // scaring in the beginning of the game.
        if (timePassed < 1f)
        {
            timePassed += Time.deltaTime;
            return;
        }

        TrackHands();
    }

    private void TrackHands()
    {
        // get the positions of each piece of tracking equipment
        UpdateVRTracking();

        bool wavingLeftHand = WavingHand(currentLeftHandPos, oldLeftPosition);
        bool wavingRightHand = WavingHand(currentRightHandPos, oldRightPosition);

        if (wavingLeftHand && wavingRightHand)
		{
            ScareSeagulls();
		}

        // change the old left and right positions to be that of the current ones.
        SetOldHandPos();
    }
    private void UpdateVRTracking()
    {
        currentHeadPos = headTransform.position;
        currentRightHandPos = rightHandTransform.position;
        currentLeftHandPos = leftHandTransform.position;
    }

    private bool WavingHand(Vector3 handPos, Vector3 oldHandPos)
    {
        // check so that the righthand is above the players head
        if (handPos.y > currentHeadPos.y)
        {
            // set the Y position to 0 (to not interfere with our velocity calculation)
            handPos.y = 0;
            // get the velocity the hand is traveling 
            float velocity = GetSpeed(handPos, oldHandPos);

            // check if the velocity is higher then the minimum for scaring birds.
            if (velocity > scareVelocityThreshold)
            {
                return true;
            }
        }
        return false;
    }
    private float GetSpeed(Vector3 currentPos, Vector3 oldPosition)
    {
        // get the distance traveled between the old position and the current position.
        float distanceTraveled = Vector3.Distance(oldPosition, currentPos);

        // turn our distanceTraveled into a positive number. As we are only 
        // working with velocity and not direction it's easier to have it as a 
        // positive number.
        distanceTraveled = Mathf.Abs(distanceTraveled);

        // return our distanceTraveled divided by Time.deltaTime to make it into a 
        // velocity based on m/s.
        return distanceTraveled / Time.deltaTime;
    }

    private void ScareSeagulls()
    {
        // cast a sphere with the radius of our scareRadius and save all collisions on
        // on the seagull layer into a Collider array
        Collider[] seagulls = Physics.OverlapSphere(headTransform.position, scareRadius, seagullLayer);

        if(seagulls.Length < 1)
		{
            return;
		}

        PlayShooSound();

        // go through the entire array
        foreach (var seagull in seagulls)
        {
            // scare the seagull
            seagull.GetComponent<SeagullController>().IsScared = true;
        }
    }
    private void PlayShooSound()
    {
        int randomSoundIndex;
        // a do-while loop that gets a random clip and does so until it 
        // gets one that isn't the previously used clip.
        do
        {
            randomSoundIndex = Random.Range(0, shooSounds.Length);
        }
        while (randomSoundIndex == lastSoundIndex);

        // sets the lastSoundIndex to be that of the randomSoundIndex (so it's updated)
        lastSoundIndex = randomSoundIndex;

        // sets the audioplayers clip to that of the element at randomSoundIndex
        // in our shooSounds array
        shooPlayer.clip = shooSounds[randomSoundIndex];

        // play the clip.
        shooPlayer.Play();
    }
}
