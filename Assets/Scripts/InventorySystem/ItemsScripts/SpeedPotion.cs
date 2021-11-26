using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increase Speed", menuName = "InventoryItem/SpeedPotions")]
public class SpeedPotion : InventoryItem
{
    public float speedIncrease = 2;
    public override bool Use(PlayerInventory inverntory)
    {
        inverntory.player.moveSpeed += speedIncrease;
        return true;
    }
}
