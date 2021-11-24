using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ScriptableObjects/Seagull Settings")]
public class SeagullSettings : ScriptableObject
{
    public float speed = 10f;
	public float endSpeed = 5f;
	public float acceleration = 0.5f;
	public float deacceleration = 0.5f;
	public float minSpeed = 1f;

	public float YOffset = 2f;
    public float distanceToFood = 3f; // maybe we can remove this?
    public float velocityLimit = 4f;
	public float diveDistance = 3f;
}
