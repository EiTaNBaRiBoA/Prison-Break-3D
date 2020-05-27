using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Audio;

public sealed class GameManager : MonoBehaviour
{    

    //singleton
    private static GameManager _instance;
    public static GameManager gameManager{ get {return _instance;}}

    //consts
    const string MUSIC_VOLUME_KEY = "MusicVolume";
    const string SFX_VOLUME_KEY = "SFXVolume";

    //cache
    public int buildIndex; //for scene
    LevelManager levelManager;
    
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;


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


    #region loading scenes
    public void RequestToLoadScene(int buildIndex)
    {
        _instance.buildIndex=buildIndex;
        SceneManager.LoadScene("Splash");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region load Settings
    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY,volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY,volume);
    }
        public void GetMusicVolume()
    {
        musicMixer.SetFloat(MUSIC_VOLUME_KEY,PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY)); 
    }
    
    public void GetSFXVolume()
    {
        sfxMixer.SetFloat(SFX_VOLUME_KEY,PlayerPrefs.GetFloat(SFX_VOLUME_KEY)); 
    }
    #endregion
}
