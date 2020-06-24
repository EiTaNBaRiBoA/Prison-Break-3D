using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandItem : MonoBehaviour
{
    public void ItemInstantiate(GameObject itemToHand,Vector3 rotation)
    {
        GameObject item = Instantiate(itemToHand,transform.position,Quaternion.identity,this.transform);
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