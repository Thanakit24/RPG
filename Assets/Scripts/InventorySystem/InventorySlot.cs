using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Item UI")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    [Header("Variables from an item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void SetUp(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager; 
        if (thisItem)
        { 
            itemImage.sprite = thisItem.itemSprite; //set this slot sprite to the item sprite
            Debug.Log("Set slot sprite to item sprite");
            itemNumberText.text = $"{thisItem.numberHeld}/{thisItem.maxNumberHeld}";
        }
    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.ItemDescriptionAndButton(thisItem.itemDescription, thisItem.usable, thisItem);
        }
    }
    
}
