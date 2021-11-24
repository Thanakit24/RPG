using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MerchantScript : InventoryManager
{
    public Interactable interactable;
    public GameObject shopUI;
    public TMP_Text itemCost;
    bool isActivated = false;
     PlayerController currentPlayer;
    public Conversation merchantConvo;
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
    void Update()
    {
        if (isActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            OnInteractFinish();
        }
    }
    void OnShopInteract(PlayerController p)
    {
        currentPlayer = p;
        DialogueManager.StartConversation(merchantConvo);
        DialogueManager.textEnd += OnDialogueFinish;
    }

    void OnDialogueFinish()
    {
        DialogueManager.textEnd -= OnDialogueFinish;
        isActivated = true;
        shopUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void OnInteractFinish()
    {
        isActivated = false;
        Time.timeScale = 1f;
        shopUI.SetActive(false);
        currentPlayer = null;
        interactable.FinishInteracting();
    }

    // Update is called once per frame
   
    public void BuyItem()
    {
        if (currentItem.itemCost > currentPlayer.invManager.currency)
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
            currentPlayer.invManager.DecreaseCurrency(currentItem.itemCost);
            DecreaseItem(currentItem);

        }
    }

    public override void ItemDescriptionAndButton(InventoryItem newItem)
    {
        base.ItemDescriptionAndButton(newItem);
        itemCost.text = $"Cost:{newItem.itemCost}"; //Interpolated string: $ at start of string, code in brackets
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
