using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    // if this is not used remove is.

    [SerializeField] float RotationPerSecond = 1;

    protected void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationPerSecond);
    }
}