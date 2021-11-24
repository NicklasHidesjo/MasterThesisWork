using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private float fadeDuration;

    
    [SerializeField] Image fadeImage;

    private string SceneToLoad;
    private bool fadeOut;
    private bool fadeIn;

    float timer = 0f;

    private void OnEnable()
    {
        GameManager.CurrentGameStatus = GameStatus.fading;
        fadeIn = true;
        timer = 0;
    }

    private void Update()
    {
        if(fadeIn)
        {
            timer += Time.deltaTime/fadeDuration;
            float alpha = Mathf.Lerp(255, 0, timer);
            Debug.Log(alpha);

            Color color = new Color(0, 0, 0, alpha /255);
            fadeImage.color = color;
            if(timer >= 1)
            {
                GameManager.SetCurrentGameStatus();
                fadeIn = false;
            }
        }
        if (fadeOut)
        {
            GameManager.CurrentGameStatus = GameStatus.fading;

            timer += Time.deltaTime / fadeDuration;
            float alpha = Mathf.Lerp(0, 255, timer);
            Color color = Color.black;
            color.a = alpha;
            fadeImage.color = color;
            
            if(timer >= 1)
            {
                fadeOut = false;
                StartCoroutine(SceneLoader.LoadSceneAsync(SceneToLoad));
            }

        }
    }


    public void ChangeScene(string scene)
    {
        timer = 0f;
        SceneToLoad = scene;
        fadeOut = true;
    }
}
