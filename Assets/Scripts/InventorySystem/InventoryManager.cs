using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    //public List<InventoryItem> myInventory = new List<InventoryItem>(); //Creates a list of slot to hold items, assign in scriptable object 
    [SerializeField] public Dictionary<InventoryItem, int> myInventory = new Dictionary<InventoryItem, int>();
    public InventoryItem currentItem;
    [SerializeField] private GameObject emptyInventorySlot;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private TextMeshProUGUI descriptionText; //itemDescription text
    [SerializeField] private GameObject useButton; //item use button if its usable

    [Header("Self Reference")]
    public PlayerController player;


    public void SetTextAndButton(string description, bool buttonActive) //??
    {
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateInventorySlots();
        SetTextAndButton("", false);
    }
    void CreateInventorySlots()
    {
        print("creating slots");
        foreach (KeyValuePair<InventoryItem, int> entry in myInventory)
        {
            print($" entry: {entry}");
            GameObject temp = Instantiate(emptyInventorySlot, inventoryContent.transform.position, Quaternion.identity); //instantiate inventory slot on the content slot
            temp.transform.SetParent(inventoryContent.transform);
            InventorySlot newSlot = temp.GetComponent<InventorySlot>();
            if (newSlot)
            {
                //Put in inventory scripatble object into INSTANTIATED THING
                newSlot.SetUp(entry.Key, entry.Value, this);
            }

        }

    }
    void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryContent.transform.childCount; i++) //Check all of the children under the content game object
        {
            Destroy(inventoryContent.transform.GetChild(i).gameObject); //Get the child 
        }
    }
    public void ItemDescriptionAndButton(string Description, bool Usable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = Description;
        useButton.SetActive(Usable);
    }
    // Update is called once per frame
    public void UseButtonPressed()
    {
        if (currentItem && myInventory.ContainsKey(currentItem))
        {
            if (!currentItem.Use(this))
            {
                return;
            }
            myInventory[currentItem] -=  1;
            if (myInventory[currentItem] == 0)
            {
                myInventory.Remove(currentItem);
            }

            ClearInventorySlots();
            CreateInventorySlots();
        }
    }

    public bool AddItem(InventoryItem item)
    {
        if (!myInventory.ContainsKey(item))
        {
            print("added new");
            myInventory.Add(item, 0);
        }
        if (myInventory[item] + 1 > item.maxNumberHeld)
        {
            return false;
        }

        myInventory[item]++;

        ClearInventorySlots();
        CreateInventorySlots();
        return true;
    }

}
