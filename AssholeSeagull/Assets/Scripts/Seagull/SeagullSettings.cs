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
}
