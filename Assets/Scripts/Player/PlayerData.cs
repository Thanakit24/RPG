using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;
    public int currency;
    public int level;

    //public List<>


    public PlayerData (PlayerController player)
    {

        //player.invManager.myInventory.Keys
        health = player.health;
        currency = player.invManager.currency;
    }

    public void OnBeforeSerialize()
    {
        Debug.Log("Before cerial");
    }

}
