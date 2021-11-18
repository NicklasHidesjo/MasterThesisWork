using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Game Settings")]
public class GameSettings : ScriptableObject
{
    public GameModes GameMode;

    public float GameDuration = 120f;
    public bool TimerOff = false;
    public bool AlwaysFreshFood = false;
    public bool SeagullsDontAttack = false;
    public int SeagullAmount = 1;
    public Vector2 SeagullSpawnInterval = new Vector2(3f,7f);
}

