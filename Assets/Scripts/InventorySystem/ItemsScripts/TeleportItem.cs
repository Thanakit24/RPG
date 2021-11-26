using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport Potion", menuName = "InventoryItem/Teleport")]
public class TeleportItem : InventoryItem
{
    public string sceneToLoad;
    public int index;
    public override bool Use(PlayerInventory inverntory)
    {
        SceneScript.instance.LoadScene(sceneToLoad, index);
        return true;
    }
}
