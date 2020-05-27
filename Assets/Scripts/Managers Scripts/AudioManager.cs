using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI

public class AudioManager : MonoBehaviour
{
    //singleton
    private static AudioManager _instance;
    public static AudioManager audioManager{ get {return _instance;}}

    //cached
    public Slider musicVol;
    public Slider sfxVol;

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

        public void OnSetSound()
        {
            GameManager.gameManager.SetMusicVolume(Mathf.Log10(musicVol.value)*20);
            GameManager.gameManager.SetSFXVolume(Mathf.Log10(sfxVol.value)*20);
            GameManager.gameManager.GetMusicVolume();
            GameManager.gameManager.GetSFXVolume();
        }



}
