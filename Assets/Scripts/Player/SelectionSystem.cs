using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSystem : MonoBehaviour
{
    public Text selectionText;
    public float selectingTries = 2;
    public float maxDistance = 5f;
    public Dictionary<string, GameObject> ownedItems = new Dictionary<string, GameObject>();
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private string actionalbeTag = "Actionable";
    private RaycastHit oldItem;
    private bool isPickable;
    private bool findAction;

    void Start()
    {
        selectionText.text = "Selection Tries left: " + selectingTries.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectingTries >= 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit item;
                item = SelectedItem(ray);
                if (item.transform != null && isPickable)
                {
                    GameObject pickedItem = item.transform.gameObject;
                    ownedItems.Add(pickedItem.GetComponent<Item>().itemPicked.currentItem.ToString(), pickedItem);
                    pickedItem.GetComponent<Item>().Picked();
                    isPickable = false;
                }
            }
            else
            {
                FindObjectOfType<MenuManager>().LosingCanvas();
                selectionText.gameObject.SetActive(false);
            }
            selectionText.text = "Selection Tries left: " + selectingTries.ToString();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (selectingTries >= 0)
            {
                findAction = false;
                if (ownedItems.Count != 0)
                {
                    foreach (var ownedItem in ownedItems)
                    {
                        string itemName = ownedItem.Value.GetComponent<Item>().itemPicked.currentItem.ToString();
                        CallForAnAction(itemName);

                    }
                }
                if (findAction == false)
                {
                    selectingTries--;
                }
            }
            else
            {
                FindObjectOfType<MenuManager>().LosingCanvas();
                selectionText.gameObject.SetActive(false);
            }
            selectionText.text = "Selection Tries left: " + selectingTries.ToString();
        }
    }

    private void CallForAnAction(string itemToUse)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit selectAction;
        if (Physics.Raycast(ray, out selectAction, maxDistance))
        {
            Transform selection = selectAction.transform;
            if (selection.CompareTag(actionalbeTag))
            {
                if (itemToUse == selectAction.transform.gameObject.GetComponent<Item>().itemPicked.currentItem.ToString())
                {
                    findAction = true;
                    // todo FindObjectOfType<> and do something/animation
                }
            }
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
                if (selectionRenderer != null && item.transform != oldItem.transform)
                {
                    oldItem = item;
                    isPickable = true;
                }
            }
            else
            {
                selectingTries--;
            }
        }
        else if (oldItem.transform != null)
        {
            oldItem = new RaycastHit();
        }
        else if (item.transform == null)
        {
            selectingTries--;
        }

        return item;
    }
}
