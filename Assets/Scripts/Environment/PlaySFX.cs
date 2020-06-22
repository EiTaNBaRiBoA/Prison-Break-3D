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
            Debug.Log("cop added to the list");
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
                Debug.Log("cop should be playing");
                cop.GetComponent<CopSFX>().NarrativeCop();
            }
            gameObject.SetActive(false);
        }
    }
}
