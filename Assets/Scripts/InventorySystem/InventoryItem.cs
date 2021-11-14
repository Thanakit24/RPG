using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryItem : ScriptableObject
{
    //Create a scriptable object and assign its values
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int numberHeld;
    public int maxNumberHeld =1;
    public bool usable;
    public bool unique;

    public virtual void Use(InventoryManager manager)
    {
        DecreaseAmount();
    }
    public void DecreaseAmount(int amountToDecrease = 1)
    {
        numberHeld -= amountToDecrease;
        if (numberHeld < 0)
        {
            numberHeld = 0;
        }
    }
}
