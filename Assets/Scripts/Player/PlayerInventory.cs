using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : InventoryManager
{
    [Header("Self Reference")]
    public PlayerController player;
    public void UseButtonPressed()
    {
        if (currentItem && myInventory.ContainsKey(currentItem))
        {
            if (!currentItem.Use(this))
            {
                return;
            }
            DecreaseItem(currentItem);
        }
    }
    public bool AddItem(InventoryItem item)
    {
        if (!myInventory.ContainsKey(item))
        {
            myInventory.Add(item, 0);
        }
        if (myInventory[item] + 1 > item.maxNumberHeld) //if the item is more than the max held then return false, meaning it doesnt do anyth
        {
            //Adding gone wrong
            return false;
        }

        myInventory[item]++;

        ClearInventorySlots();
        CreateInventorySlots();
        return true;
    }
}
