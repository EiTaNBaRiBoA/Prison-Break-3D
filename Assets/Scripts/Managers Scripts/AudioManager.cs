using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //singleton
    private static AudioManager _instance;
    public static AudioManager audioManager{ get {return _instance;}}

    public AudioClip[] music;
    public AudioClip[] sfx;


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
        public void SetMusic(float musicVol)
        {
            GameManager.gameManager.SetMusicVolume(musicVol);
            GameManager.gameManager.GetMusicVolume(Mathf.Log10(musicVol)*20);
            
        }
        public void SetSFX(float sfxVol)
        {
            GameManager.gameManager.SetSFXVolume(sfxVol);
            GameManager.gameManager.GetSFXVolume(Mathf.Log10(sfxVol)*20);
        }



}
