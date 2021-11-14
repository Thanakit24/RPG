using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "InventoryItem/New Item")]
public class HealthPotion : InventoryItem
{
    public int healthIncrease = 3;

    public override void Use(InventoryManager manager)
    {
        if (manager.player.health == manager.player.MaxOfHearts)
            return;
        base.Use(manager);
        manager.player.health += healthIncrease;
        Debug.Log("called and increase player hp");
    }

 
}
