using UnityEngine;

public class InteractionController : MonoBehaviour {
    Inventory inventory;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    public void ApplyInteraction(Item usedItem)
    {
        if (usedItem != null)
        {
            int itemID = usedItem.assignedID;
            string itemName = usedItem.itemName;
            GameObject itemObj = usedItem.itemObject;

            Debug.Log(itemName + " ID: " + itemID);
            switch (itemID)
            {
                case 1:
                    Instantiate(itemObj, transform.position, itemObj.transform.rotation); //Bombs action is to be instantiated
                    inventory.RemoveItem();
                    break;

                default:
                    Debug.Log("No item with the current ID yet, or case is not assigned...");
                    break;
            }
        }
    }
}
