using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSystem : MonoBehaviour
{
    public float maxDistance=5f;
    public Dictionary<string,GameObject> ownedItems = new Dictionary<string, GameObject>();
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private Material highLightMaterial;
    [SerializeField] private Material oldMaterial;
    private RaycastHit oldItem;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit item;
            item = SelectedItem(ray);
            if(item.transform!=null&&item.transform.CompareTag(selectableTag)){
            GameObject pickedItem =  item.transform.gameObject;
            ownedItems.Add(pickedItem.GetComponent<Item>().itemPicked.currentItem.ToString(),pickedItem);
            pickedItem.GetComponent<Item>().Picked();
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            foreach (var ownedItem in ownedItems)
            {
                string itemName = ownedItem.Value.GetComponent<Item>().itemPicked.currentItem.ToString();
                Debug.Log(itemName);
                CallForAnAction(itemName);
            }
        }
    }

        private void CallForAnAction(string itemToUse)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit selectAction;
            if(Physics.Raycast(ray,out selectAction,maxDistance))
            {
                //todo action list to be able to check if can be used like doors and etc;
                return;
            }

        }

    private RaycastHit SelectedItem(Ray ray)
    {
        RaycastHit item;
        if (Physics.Raycast(ray, out item, maxDistance))
        {
            Transform selection = item.transform;
            if (selection.CompareTag(selectableTag))
            {
                Renderer selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null&&item.transform != oldItem.transform)
                {
                oldItem=item;
                oldMaterial = selectionRenderer.material;
                selectionRenderer.material = highLightMaterial;
                }
            }
        }
        else if (oldItem.transform!=null)
        {
            oldItem.transform.GetComponent<Renderer>().material = oldMaterial;
            oldItem = new RaycastHit();
        }

        return item;
    }
}
