using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increase Attack", menuName = "InventoryItem/AttackPotions")]
public class AttackBuffPotion : InventoryItem
{
    public int attackIncrease = 1;
    [ColorUsage(true, true)]
    public Color increaseColor;
    public override bool Use(PlayerInventory inverntory)
    {
        inverntory.player.attack += attackIncrease;
        inverntory.player.swordSr.material.SetColor("_Color", increaseColor);
        return true;
    }
}
