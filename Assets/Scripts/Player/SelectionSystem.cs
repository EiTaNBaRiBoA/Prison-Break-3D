using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSystem : MonoBehaviour
{
    public float maxDistance = 5f;
    public Dictionary<string, GameObject> ownedItems = new Dictionary<string, GameObject>();
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string actionalbeTag = "Actionable";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RayCastItem();
        }
    }




    private void RayCastItem()
    {
        //Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray mouseRay = new Ray(Camera.main.transform.position,Camera.main.transform.forward);
        RaycastHit selectionCast;
        if (Physics.Raycast(mouseRay, out selectionCast, maxDistance, ~(1<<9)))
        {
            GameObject item = selectionCast.transform.gameObject;
            if (item.CompareTag(selectableTag))
            {
                PickItem(item);
            }
            else if (item.CompareTag(actionalbeTag))
            {
                if (ownedItems.Count != 0)
                {
                    foreach (var ownedItem in ownedItems)
                    {
                        string itemName = ownedItem.Value.GetComponent<Item>().itemPicked.currentItem.ToString();
                        if (itemName == item.GetComponent<Item>().itemPicked.currentItem.ToString())
                        {
                            item.GetComponent<Item>().Picked();
                            FindObjectOfType<HandItem>().ItemDestroy();
                        }
                    }
                }
            }
        }
    }
    private void PickItem(GameObject item)
    {
        ownedItems.Add(item.GetComponent<Item>().itemPicked.currentItem.ToString(), item);
        item.GetComponent<Item>().Picked();
        FindObjectOfType<HandItem>().ItemDestroy();
        FindObjectOfType<HandItem>().ItemInstantiate(item,item.GetComponent<Item>().localRotation();
    }
}
