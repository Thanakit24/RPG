using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Sample Item", menuName = "InventoryItem/Standard")]
public class InventoryItem : ScriptableObject
{
    //Create a scriptable object and assign its values
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    //public int numberHeld;
    public int maxNumberHeld = 1;
    public bool usable;
    public bool unique;

    public virtual bool Use(InventoryManager manager)
    {
        //Nothing
        Debug.Log("No override");
        return false;

    }
    
}
