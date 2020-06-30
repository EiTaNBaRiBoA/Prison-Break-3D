using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickable : MonoBehaviour,IPickable
{

    public void Picked()
    {
        gameObject.SetActive(false);
    }
}
