using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Incrase Health", menuName = "InventoryItem/HEalthIncrease")]
public class HealthIncrease : InventoryItem
{
    public override bool Use(PlayerInventory inverntory)
    {
        inverntory.player.MaxOfHearts++;
        return true;
    }
}
