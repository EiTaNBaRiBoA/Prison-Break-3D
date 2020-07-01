using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //singleton
    private static AudioManager _instance;
    public static AudioManager audioManager { get { return _instance; } }

    private AudioSource audioSource;
    public AudioClip[] musics;


    private void Awake()
    {
        int? checking = FindObjectsOfType<GameManager>()?.Length;
        if (checking > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            audioSource = FindObjectOfType<AudioSource>();
            _instance = this;
        }
    }
    public void SetMusic(float musicVol)
    {
        GameManager.gameManager.SetMusicVolume(musicVol);
        GameManager.gameManager.GetMusicVolume(Mathf.Log10(musicVol) * 20);

    }
    public void SetSFX(float sfxVol)
    {
        GameManager.gameManager.SetSFXVolume(sfxVol);
        GameManager.gameManager.GetSFXVolume(Mathf.Log10(sfxVol) * 20);
    }

    public void ChangeMusic(int incomeMusic)
    {
        audioSource.clip = musics[incomeMusic];
        audioSource.loop = true;
        audioSource.Play();
    }
    public void NarrativeSFX(AudioClip playerClip)
    {
        FindObjectOfType<PlayerMusic>().PlayerSFX(playerClip);
    }
}
