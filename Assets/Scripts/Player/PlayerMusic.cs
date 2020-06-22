using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMusic : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayerSFX(AudioClip sfx)
    {
        if (sfx != null)
        {
            audioSource.clip = sfx;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
