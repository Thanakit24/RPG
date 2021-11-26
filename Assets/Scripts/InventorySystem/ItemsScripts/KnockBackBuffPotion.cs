using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increase Knockback", menuName = "InventoryItem/Knockback")]
public class KnockBackBuffPotion : InventoryItem
{
    public float boop = 10f;
    public override bool Use(PlayerInventory inverntory)
    {
        inverntory.player.knockBack += boop;
        return true;
    }
}
