using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour, IPickable
{


    public void Picked()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(transform.parent!=null){
            Ray shooting = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(shooting, out hit, 5f))
                {
                    if (hit.transform.gameObject.CompareTag("Cop"))
                    {
                        hit.transform.gameObject.SetActive(false);
                    }

                }
            }
        }
    }
}
