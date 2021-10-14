using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerRotation : MonoBehaviour
{
    float time;
    float totalTime;
    void Start()
    {
        totalTime = FindObjectOfType<GameManager>().GameDuration;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime / totalTime;

        float zRotation = Mathf.Lerp(0, 360, time);
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = zRotation;
        transform.eulerAngles = rotation;
    }
}
