using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject {
    public Sprite sprite;
    public GameObject itemObject;
    public string itemName;
    public int assignedID;
    public bool isStackable;
}
