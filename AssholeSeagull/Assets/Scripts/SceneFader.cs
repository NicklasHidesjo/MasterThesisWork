using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private float fadeDuration;

    private void OnEnable()
    {
        GameManager.CurrentGameStatus = GameStatus.fading;

        // do our fading here 

        GameManager.SetCurrentGameStatus();
    }

    public void ChangeScene(string Scene)
    {

    }
}
