using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] float RotationPerSecond = 1;

    protected void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
    }
}