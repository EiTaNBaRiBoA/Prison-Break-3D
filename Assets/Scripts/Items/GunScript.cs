using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour, IPickable
{

    public GameObject shotVFX;
    public Transform shotArea;
    private AudioSource audioSource;
    public AudioClip shot;
    private bool isPicked = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = shot;
        audioSource.loop = false;
    }
    public void Picked()
    {
        gameObject.SetActive(false);
    }

    private void OnPicked()
    {
        transform.localScale /= 3;
    }
    private void Update()
    {
        if (transform.parent.gameObject == FindObjectOfType<HandItem>().gameObject)
        {
            if (!isPicked)
            {
                OnPicked();
                isPicked = true;
            }
            Ray shooting = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(shotVFX, shotArea.position, Quaternion.identity, shotArea.transform);
                audioSource.PlayOneShot(shot);
                if (Physics.Raycast(shooting, out hit, 20f))
                {
                    if (hit.transform.gameObject.CompareTag("Cop"))
                    {
                        hit.transform.gameObject.GetComponent<CopSFX>().OnDeath();
                    }
                }
            }
        }
    }
}
