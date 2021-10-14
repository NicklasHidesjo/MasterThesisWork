using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSeagull : MonoBehaviour
{
    [SerializeField] float scareRadius;
    [SerializeField] LayerMask seagullLayer;

    [SerializeField] float scareVelocityThreshold;

    [SerializeField] Transform headTransform;
    [SerializeField] Transform rightHandTransform;
    [SerializeField] Transform leftHandTransform;

    [SerializeField] AudioClip[] shooSounds;
    [SerializeField] AudioSource shooPlayer;

    Vector3 oldLeftPosition;
    Vector3 oldRightPosition;


    float timePassed = 0; // remove this and make it so that scaring wont get tricked at start

    int lastShooSoundUsed = -1;

    void Start()
    {
        oldLeftPosition = leftHandTransform.position;
        oldRightPosition = rightHandTransform.position;
    }

    void Update()
    {
        if(timePassed < 1f)
		{
            timePassed += Time.deltaTime;
            return;
		}

        Vector3 headPos = headTransform.position;
        Vector3 rightHandPos = rightHandTransform.position;
        Vector3 leftHandPos = leftHandTransform.position;


        if (rightHandPos.y > headPos.y)
        {
            rightHandPos.y = 0;
            float velocity = GetSpeed(rightHandPos, oldRightPosition);

            if(velocity > scareVelocityThreshold)
            {
                ScareSeagulls(rightHandTransform.position);
            }
        }
        if (leftHandPos.y > headPos.y)
        {
            leftHandPos.y = 0;
            float velocity = GetSpeed(leftHandPos, oldLeftPosition);

            if (velocity > scareVelocityThreshold)
            {
                ScareSeagulls(leftHandTransform.position);
            }
        }

        oldLeftPosition = leftHandPos;
        oldLeftPosition.y = 0;
        oldRightPosition = rightHandPos;
        oldRightPosition.y = 0;
    }

    private void ScareSeagulls(Vector3 position)
    {
        Collider[] seagulls = Physics.OverlapSphere(position, scareRadius, seagullLayer);
        foreach (var seagull in seagulls)
        {
            seagull.GetComponent<SeagullMovement>().Scared();
        }

        PlayShooSound();
    }

    private void PlayShooSound()
    {
        int randomClip = 0;
        do
        {
            randomClip = Random.Range(0, shooSounds.Length);
        }
        while (randomClip == lastShooSoundUsed);
        lastShooSoundUsed = randomClip;
        shooPlayer.clip = shooSounds[randomClip];
        shooPlayer.Play();
    }

    private float GetSpeed(Vector3 currentPos, Vector3 oldPosition)
    {
        float distanceTraveled = Vector3.Distance(oldPosition, currentPos);
        distanceTraveled = Mathf.Abs(distanceTraveled);
        return distanceTraveled / Time.deltaTime;
    }
}
