using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip copClip;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
        AudioManager.audioManager.NarrativeSFX(audioClip,copClip);
        gameObject.SetActive(false);
        }
    }
}
