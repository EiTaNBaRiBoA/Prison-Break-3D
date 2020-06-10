using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject button;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(button.activeSelf==false)
        {
            GetComponent<Animator>().SetTrigger("GoDown");
        }
    }
}
