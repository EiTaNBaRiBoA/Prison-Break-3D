using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public sealed class GameManager : MonoBehaviour
{    
    private static GameManager _instance;
    public static GameManager gameManager{ get {return _instance;}}

    LevelManager levelManager;

    private void Awake() {
        if(_instance==null)
        {
            _instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelManager.LoadingNextScene(5f);
    }


}
