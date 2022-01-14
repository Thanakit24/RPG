using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int maxHealth;
    public int currency;
    public List<string> inventoryKeys = new List<string>();
    public List<int> inventoryValues = new List<int>();

    //DO NOT Populate IN SCENETRANSITION
    public int level;
    public List<int> loadedScenes;
    public List<int>[] sceneItems;
    

    public PlayerData(PlayerController player)
    {
        health = player.health;
        maxHealth = player.MaxOfHearts;
        currency = player.invManager.currency;

        foreach (InventoryItem item in player.invManager.myInventory.Keys)
        {
            inventoryKeys.Add(item.Id);
            //Value indexed from dictionary
            int value = player.invManager.myInventory[item];
            inventoryValues.Add(value);
        }

        loadedScenes = SceneScript.instance.loadedScenes;
    }
    public void PopulatePlayer(PlayerController player)
    {
        player.health = health;
        player.invManager.currency = currency;
        player.MaxOfHearts = maxHealth;
        for (int i = 0; i < inventoryKeys.Count; i++)
        {
            string id = inventoryKeys[i]; //this is how u iterate
            int value = inventoryValues[i];
            //Find item based on id from ItemCache
            InventoryItem item = null;
            for (int j = 0; j < ItemCache.instance.items.Count; j++)
            {
                InventoryItem currentItem = ItemCache.instance.items[j];
                if (currentItem.Id == id)
                {
                    item = currentItem;
                    break;
                }
            }
            if (item)
            {
                //InventoryItem item = ItemCache.instance.items.Find(x => x.Id == id); //find an id that matches the id
                player.invManager.myInventory.Add(item, value);
            }
        }
    }

    public void OnBeforeSerialize()
    {
        Debug.Log("Before cerial");
    }

}
