using UnityEngine;

public class CollectionObjective : MonoBehaviour
{
    public string title; //Quest title
    public string description; //Quest description 
    public bool isComplete; //Is the quest completed?
    public string actionVerb; //Verb to describe quest action
    public int collectionAmount; //Amount of items to collect in the quest
    public int currentAmount; //Current amount of collected items
    public Item itemToCollect; //The object (Item) that will have to be collected
    Inventory inventory;
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    public void UpdateProgress()
    {
        //I will probably need to update progress each time the item is taken, but for now its ok
        if (inventory.Contains(itemToCollect, collectionAmount))
        {
            Debug.Log("Collected " + collectionAmount + " " + itemToCollect.itemName + "s");
            isComplete = true;
        }
    }

    private void Update()
    {
        if(!isComplete)
            UpdateProgress();
    }

    //Info string
    public override string ToString()
    {
        //Ex: '8/10 feathers gathered'
        return currentAmount + "/" + collectionAmount + " " + itemToCollect.itemName + " " + actionVerb + "ed!";
    }
     
}
