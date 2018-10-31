using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static GameObject gameCanvas;
     
    public const int numItemSlots = 4;

    GameObject whiteBorder;

    #region Arrays_For_items
    public Image[] itemImages = new Image [numItemSlots];
    public Item[] items = new Item[numItemSlots];
    public GameObject[] itemSlots = new GameObject[numItemSlots];
    public Text[] itemStackNumTexts = new Text[numItemSlots];
    public int[] itemStackNums = new int[numItemSlots];
    #endregion

    public float slotScaleFactor = 1.1f;
    //private static bool created = false;
    private int selectionIndex = 0;

    private InteractionController interactionController;

    public static bool isFull = false;

    void Awake()
    {
        if (gameCanvas == null)
        {
            DontDestroyOnLoad(gameObject);
            gameCanvas = this.gameObject;
        }
        else if(gameCanvas != this.gameObject)
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        interactionController = FindObjectOfType<InteractionController>();
      
        whiteBorder = GameObject.FindWithTag("WhiteBorder");
        whiteBorder.SetActive(true);

        SelectItemSlot();
        for (int i = 0; i < numItemSlots; i++)
        {
            itemStackNumTexts[i].gameObject.SetActive(false);
        }
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

    public bool Contains(Item itemToCheck, int itemAmount)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].assignedID == itemToCheck.assignedID && itemStackNums[i] == itemAmount)
            {
                return true;
            }
        }
        
        return false;
    }

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && itemToAdd.assignedID == items[i].assignedID && items[i].isStackable)
            {
                itemStackNums[i]++;
                itemStackNumTexts[i]
                    .text =
                    itemStackNums[i]
                    .ToString();
                return;
            }
            else if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;
                itemStackNums[i]++;
                if (items[i].isStackable)
                {
                    itemStackNumTexts[i].gameObject.SetActive(true);
                }
                itemStackNumTexts[i].text = itemStackNums[i].ToString();
                return;
            }
        }
    }

    public void RemoveItem()
    {
        int i = selectionIndex;
        if (items[i].isStackable && itemStackNums[i] > 1)
        {
            itemStackNums[i]--;
            itemStackNumTexts[i].text = itemStackNums[i].ToString();
            return;
        }
        else {
            items[i] = null;
            itemStackNums[i]--;
            itemImages[i].sprite = null;
            itemImages[i].enabled = false;
            itemStackNumTexts[i].gameObject.SetActive(false);        
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                if (items[i].isStackable && itemStackNums[i] > 1)
                {
                    itemStackNums[i]--;
                    itemStackNumTexts[i].text = itemStackNums[i].ToString();
                    return;
                }
                else
                {
                    items[i] = null;
                    itemStackNums[i]--;
                    itemImages[i].sprite = null;
                    itemImages[i].enabled = false;
                    itemStackNumTexts[i].gameObject.SetActive(false);
                }
            }
        }
    }



    private void GetSelectionIndex()
    {

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
        whiteBorder.SetActive(true);
        whiteBorder.transform.position = itemSlots[i].transform.position;
    }

    private void ResetScaledSlot()
    {
        whiteBorder.SetActive(false);    
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (items[selectionIndex] != null)
            {
                int i = selectionIndex;
                interactionController.ApplyInteraction(items[i]);
            }
        }
    }
}
