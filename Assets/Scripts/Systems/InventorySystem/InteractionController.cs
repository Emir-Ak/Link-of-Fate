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
                case 2:
                    Debug.Log("Hello Developer! This is a testBomb which isn't stackable it has differet ID from normal Bomb and therefore no reaction is available. It is used to test on behaviour of stackable objects with non-stackable" +
                        "\n...and so far, so good!");
                    break;

                default:
                    Debug.Log("No item with the current ID yet, or case is not assigned...");
                    break;
            }
        }
    }
}
