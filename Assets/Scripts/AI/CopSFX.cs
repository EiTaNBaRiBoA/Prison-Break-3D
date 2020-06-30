using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopSFX : MonoBehaviour
{
    private AudioSource audioSource;
    private NavMeshAgent agent;
    public AudioClip footsteps;
    //public AudioClip sfx; to be modified to "I See you"
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        agent = FindObjectOfType<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null)
        {
            if (agent.speed > 0f && !audioSource.isPlaying)
            {
                audioSource.clip = footsteps;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void NarrativeCop(AudioClip copClip)
    {
        if (copClip != null)
        {
            audioSource.Stop();
            audioSource.clip = copClip;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
