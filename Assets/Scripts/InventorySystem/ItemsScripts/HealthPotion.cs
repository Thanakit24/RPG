using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "InventoryItem/HealthPotion")]
public class HealthPotion : InventoryItem
{
    public int healthIncrease = 3;

    public override bool Use(PlayerInventory inventory)
    {
        if (inventory.player.health + healthIncrease > inventory.player.MaxOfHearts)
            return false;
        //base.Use(manager);
        inventory.player.health += healthIncrease;
        Debug.Log("called and increase player hp");
        return true;
    }

 
}
