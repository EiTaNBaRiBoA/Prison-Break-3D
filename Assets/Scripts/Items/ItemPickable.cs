using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickable : MonoBehaviour,IPickable
{
    private AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        audioSource=GetComponent<AudioSource>();
        audioSource.loop= false;
        audioSource.clip = audioClip;
    }
    public void Picked()
    {
        gameObject.SetActive(false);
    }
}
