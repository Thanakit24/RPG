using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCache : MonoBehaviour
{
    public static ItemCache instance;
    public List<InventoryItem> items;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
