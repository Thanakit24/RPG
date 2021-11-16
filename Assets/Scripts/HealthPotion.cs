using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUp", menuName = "InventoryItem/HealthPotion")]
public class HealthPotion : InventoryItem
{
    public int healthIncrease = 3;

    public override bool Use(InventoryManager manager)
    {
        if (manager.player.health + healthIncrease > manager.player.MaxOfHearts)
            return false;
        //base.Use(manager);
        manager.player.health += healthIncrease;
        Debug.Log("called and increase player hp");
        return true;
    }

 
}
