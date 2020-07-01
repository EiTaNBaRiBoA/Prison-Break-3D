using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CopSFX : MonoBehaviour
{
    private AudioSource audioSource;
    private NavMeshAgent agent;
    public AudioClip footsteps;
    public GameObject copSpeaking;
    //public AudioClip sfx; to be modified to "I See you"
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null && audioSource.enabled == true)
        {
            if (agent.speed > 0f && !audioSource.isPlaying)
            {
                audioSource.clip = footsteps;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    public void NarrativeCop(AudioClip[] copClip)
    {
        if (copClip != null && audioSource.enabled == true && !copSpeaking.GetComponent<AudioSource>().isPlaying)
        {
            int rand = Random.Range(0,copClip.Length);
            copSpeaking.GetComponent<AudioSource>().clip=copClip[rand];
            copSpeaking.GetComponent<AudioSource>().Play();
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
