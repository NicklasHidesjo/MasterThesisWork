using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSeagull : MonoBehaviour
{
    
    [Tooltip("The radius in unity units that you can scare the bird from")]
    [SerializeField] float scareRadius;
    [Tooltip("The layer that the seagulls are at")]
    [SerializeField] LayerMask seagullLayer;

    [Tooltip("The minimum velocity the players hands need to have to scare the bird")]
    [SerializeField] float scareVelocityThreshold;

    [SerializeField] Transform headTransform;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] Transform leftHandTransform;

    [SerializeField] AudioClip[] shooSounds;
    [SerializeField] AudioSource shooPlayer;

    // the old positions of the left and right hand (used when tracking velocity)
    Vector3 oldLeftPosition;
    Vector3 oldRightPosition;

    float timePassed = 0; // remove this and make it so that scaring wont get triggered at start

    // a tracker to keep the same shooSound to be played twice.
    int lastShooSoundUsed = -1;

    void Start()
    {
        // set the old positions of the left and right hand.
        oldLeftPosition = leftHandTransform.position;
        oldRightPosition = rightHandTransform.position;
    }

    void Update()
    {
        // this should be removed once we find and fix the bug where you trigger
        // scaring in the beginning of the game.
        if(timePassed < 1f) 
		{
            timePassed += Time.deltaTime;
            return;
		}

        // get the positions of each piece of tracking equipment
        Vector3 headPos = headTransform.position;
        Vector3 rightHandPos = rightHandTransform.position;
        Vector3 leftHandPos = leftHandTransform.position;

        // check so that the righthand is above the players head
        if (rightHandPos.y > headPos.y)
        {
            // set the Y position to 0 (to not interfere with our velocity calculation)
            rightHandPos.y = 0;
            // get the velocity the hand is traveling
            float velocity = GetSpeed(rightHandPos, oldRightPosition);

            // check if the velocity is higher then the minimum for scaring birds.
            if(velocity > scareVelocityThreshold)
            {
                // start scaring birds.
                ScareSeagulls(rightHandTransform.position);
            }
        }
        // this is the same but for the left hand
        // (consider making it one method with different parameters in)
        if (leftHandPos.y > headPos.y)
        {
            leftHandPos.y = 0;
            float velocity = GetSpeed(leftHandPos, oldLeftPosition);

            if (velocity > scareVelocityThreshold)
            {
                ScareSeagulls(leftHandTransform.position);
            }
        }

        // change the old left and right positions to be that of the current ones.
        oldLeftPosition = leftHandPos;
        oldLeftPosition.y = 0;
        oldRightPosition = rightHandPos;
        oldRightPosition.y = 0;
    }

    private void ScareSeagulls(Vector3 position)
    {
        // cast a sphere with the radius of our scareRadius and save all collisions on
        // on the seagull layer into a Collider array
        Collider[] seagulls = Physics.OverlapSphere(position, scareRadius, seagullLayer);
        
        // go through the entire array
        foreach (var seagull in seagulls)
        {
            // scare the seagull
            seagull.GetComponent<SeagullMovement>().Scared();
        }

        // play our shooSound effect
        PlayShooSound();
    }

    private void PlayShooSound()
    {

        int randomClip = 0; // rename this better
        // a do-while loop that gets a random clip and does so until it 
        // gets one that isn't the previously used clip.
        do
        {
            randomClip = Random.Range(0, shooSounds.Length);
        }
        while (randomClip == lastShooSoundUsed);

        // sets the lastShooSounUsed to be that of the randomClip (so it's updated)
        lastShooSoundUsed = randomClip;

        // sets the audioplayers clip to that of the element of randomClip
        // in our shooSounds array
        shooPlayer.clip = shooSounds[randomClip];

        // play the clip.
        shooPlayer.Play();
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
}
