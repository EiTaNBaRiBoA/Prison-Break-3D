using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Slider loadingSlider;

    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;

    }
    void Start()
    {
        //AudioManager.audioManager.ChangeMusic("Main");
        LoadingNextScene(GameManager.gameManager.buildIndex,2f);
    }
    public void LoadingNextScene(int buildindex, float timeToWait)
    {
        StartCoroutine(LoadAsyncNextLevel(buildindex,timeToWait));
    }

    IEnumerator LoadAsyncNextLevel(int buildIndex,float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        AsyncOperation LoadingScene = SceneManager.LoadSceneAsync(buildIndex + 1);
        while(!LoadingScene.isDone)
        {
            loadingSlider.value = Mathf.Clamp01(LoadingScene.progress/0.9f);
            yield return null;
        }
    }
}
