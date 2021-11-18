﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantScript : InventoryManager
{
    public Interactable interactable;
    public GameObject shopUI;
    bool isActivated = false;
     PlayerController currentPlayer;
    //public ShopUI shop;
    // Start is called before the first frame update

    private void OnEnable()
    {
        interactable.action += OnShopInteract;
    }

    private void OnDisable()
    {
        interactable.action -= OnShopInteract;
    }
    protected override void Start()
    {

        base.Start();

    }

    void OnShopInteract(PlayerController p)
    {
        currentPlayer = p;
        isActivated = true;
        shopUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            isActivated = false;
            Time.timeScale = 1f;
            shopUI.SetActive(false);
            currentPlayer = null;
        }
    }
    public void BuyItem()
    {
        if (currentItem.itemCost > PlayerCurrency.instance.currency)
        {
            return;
        }
        else
        {
            if (!currentPlayer.invManager.AddItem(currentItem))
            {
                //PURCHASE GONE WRONG 
                return;
            }
            PlayerCurrency.instance.DecreaseCurrency(currentItem.itemCost);
            DecreaseItem(currentItem);

        }
    }
    //public void DecreaseItem()
    //{
    //    if (myInventory[item] + 1 > item.maxNumberHeld)
    //    {
    //        return false;
    //    }

    //    myInventory[item]++;

    //    ClearInventorySlots();
    //    CreateInventorySlots();
    //    return true;
    //}
}
