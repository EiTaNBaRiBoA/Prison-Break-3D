using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioClip copClip;

    private List<Collider> copColliders = new List<Collider>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cop"))
        {
            if (!copColliders.Contains(other))
            {
                copColliders.Add(other);
            }
        }
        if (other.CompareTag("Player"))
        {
            AudioManager.audioManager.NarrativeSFX(audioClip);
            foreach (Collider cop in copColliders)
            {
                if (cop.gameObject ==null)
                {
                    copColliders.Remove(cop);
                }
                else
                {
                    cop.GetComponent<CopSFX>().NarrativeCop(copClip);
                }
            }
            gameObject.SetActive(false);
        }
    }
}
