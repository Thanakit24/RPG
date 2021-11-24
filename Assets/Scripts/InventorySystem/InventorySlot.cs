using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Item UI")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private TextMeshProUGUI itemCostText; 
    [SerializeField] private Image itemImage;

    [Header("Variables from an item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    public void SetUp(InventoryItem newItem, int numberHeld, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager; 
        if (thisItem)
        { 
            itemImage.sprite = thisItem.itemSprite; //set this slot sprite to the item sprite
            Debug.Log("Set slot sprite to item sprite");
            itemNumberText.text = $"{numberHeld}/{thisItem.maxNumberHeld}";
        }
    }

    public void ClickedOn()
    {
        if (thisItem)
        {
            thisManager.ItemDescriptionAndButton(thisItem);
        }
    }
    
}
