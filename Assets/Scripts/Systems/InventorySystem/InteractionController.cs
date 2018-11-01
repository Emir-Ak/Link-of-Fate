using UnityEngine;

public class InteractionController : MonoBehaviour {
    Inventory inventory;
    PlayerController player;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<PlayerController>();
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
                case 2: //Use for exchange for princess
                    break;
                case 3: Instantiate(itemObj, transform.position + new Vector3(0f, 1f, 0f), itemObj.transform.rotation);
                    inventory.RemoveItem();
                    break;
                case 4:
                    if(player.LivingBeingType == Alive.LivingBeings.Player)
                    {
                        player.LivingBeingType = Alive.LivingBeings.Goblin;
                    }
                    else if(player.LivingBeingType == Alive.LivingBeings.Goblin)
                    {
                        player.LivingBeingType = Alive.LivingBeings.Player;
                        inventory.RemoveItem(usedItem);
                    }
                    break;
                default:
                    Debug.Log("No item with the current ID yet, or case is not assigned...");
                    break;
            }
        }
    }
}
