using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public sealed class GameManager : MonoBehaviour
{    
    private static GameManager _instance;
    public static GameManager gameManager{ get {return _instance;}}

    public int buildIndex;
    LevelManager levelManager;

    private void Awake() {
        int? checking = FindObjectsOfType<GameManager>()?.Length;
        if (checking > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
    }

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RequestToLoadScene(int buildIndex)
    {
        _instance.buildIndex=buildIndex;
        SceneManager.LoadScene("Splash");
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
