using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Teleport", menuName = "InventoryItem/TeleportToken")]
public class TeleportToken : InventoryItem
{
    public int transitionOnLoad;
    public string sceneName;

    public override bool Use(PlayerInventory inventory)
    {
        SceneScript.instance.LoadScene(sceneName, transitionOnLoad);
        return true;
    }


}
