using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
        public void LoadingNextScene(float timeToWait)
    {
        StartCoroutine(LoadAsyncNextLevel(SceneManager.GetActiveScene().buildIndex,timeToWait));
    }

    IEnumerator LoadAsyncNextLevel(int sceneIndex,float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        AsyncOperation LoadingScene = SceneManager.LoadSceneAsync(sceneIndex + 1);

        while(!LoadingScene.isDone)
        {
            float progress = Mathf.Clamp01(LoadingScene.progress/0.9f);
            Debug.Log(progress);
            yield return null;
        }
    }
}
