using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public const int numItemSlots = 4;

    public Image[] itemImages = new Image [numItemSlots];
    public Item[] items = new Item[numItemSlots];
    public GameObject[] itemSlots = new GameObject[numItemSlots];

    public float slotScaleFactor = 1.25f;
    //private static bool created = false;
    private int selectionIndex = 0;

    private InteractionController interactionController;

    public static bool isFull = false;
    //void Awake()
    //{
    //    if (!created)
    //    {
    //        DontDestroyOnLoad(gameObject);
    //        created = true;
    //    }
    //}
    
    private void Start()
    {
        interactionController = FindObjectOfType<InteractionController>();
        SelectItemSlot();
    }

    private void Update()
    {
        GetSelectionIndex();
        Interact();
        if (Input.GetKeyDown(KeyCode.R))
        {
            RemoveItem();
        }
    }

    public void AddItem(Item itemToAdd) { 
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i]  == null)
            {
                Debug.Log("Added");
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                return;
            }
        }
    }

    public void RemoveItem()
    {
        int i = selectionIndex;
        items[i] = null;
        itemImages[i].sprite = null;
        itemImages[i].enabled = false;
    }

    

    private void GetSelectionIndex()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{


        //}
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetKeyDown(KeyCode.E)) // forward
        {
            ResetScaledSlot();
            if (selectionIndex == 3)
            {
                selectionIndex = 0;
            }
            else
            {
                selectionIndex++;
            }
            SelectItemSlot(); ;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetKeyDown(KeyCode.Q)) // backwards
        {
            ResetScaledSlot();
            if (selectionIndex == 0)
            {
                selectionIndex = 3;
            }
            else
            {
                selectionIndex--;
            }
            SelectItemSlot();
        }
    }

    
    private void SelectItemSlot()
    {
        int i = selectionIndex;
        itemSlots[i].transform.localScale *= slotScaleFactor;
    }
    private void ResetScaledSlot()
    {
        int i = selectionIndex;
        itemSlots[i].transform.localScale /= slotScaleFactor;
    }
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(2) && items[selectionIndex] != null)
        {
            int i = selectionIndex;
            interactionController.ApplyInteraction(items[i]);
        }
    }
}
