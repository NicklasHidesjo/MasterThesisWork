using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    public GameModes GameMode;

    public float GameDuration = 60f;
    public bool TimerOff = false;
    public bool AlwaysFreshFood = false;
    public bool SeagullsDontAttack = true;
    public int SeagullAmount = 1;
    public float SeagullSpawnInterval = 5f;
}

public enum GameModes
{
    normal, 
    sandbox,
    peaceful,
    chaos
}
