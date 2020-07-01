using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItem : MonoBehaviour
{
    public Transform handLocation;
    public void ItemInstantiate(GameObject itemToHand,Vector3 rotation,Vector3 position)
    {
        GameObject item = Instantiate(itemToHand,handLocation.position,Quaternion.identity,this.transform);
        item.transform.localPosition += position;
        item.transform.localRotation= Quaternion.Euler(rotation);
        item.SetActive(true);
    }
    public void ItemDestroy()
    {
        if(transform.childCount>0){
        Transform item = transform.GetChild(0);
        Destroy(item.gameObject);
        }
    }
}