using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script is not in use atm 
public class LostItemReaction : MonoBehaviour {

    public Item item;

    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
    }
    
    private void ImmediateReaction() //Whenever the item is lost
    {
        inventory.RemoveItem();
    }
}





























































































































//...and the fuck why are you looking into my scripts Stephan?