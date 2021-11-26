using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increase Attack", menuName = "InventoryItem/BuffPotions")]
public class KnockBackBuffPotion : InventoryItem
{
    public int attackIncrease = 1;
    public override bool Use(PlayerInventory inverntory)
    {
        inverntory.player.knockBack += 10;
        //inverntory.player.attack += attackIncrease;
        return true;
    }
}
