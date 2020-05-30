using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionSystem : MonoBehaviour
{
    public float maxDistance=5f;
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
