using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    //public List<InventoryItem> myInventory = new List<InventoryItem>(); //Creates a list of slot to hold items, assign in scriptable object 
    public List<InventoryItem> initialList;
    [SerializeField] public Dictionary<InventoryItem, int> myInventory = new Dictionary<InventoryItem, int>();
    public InventoryItem currentItem;
    [SerializeField] private GameObject emptyInventorySlot;
    [SerializeField] private GameObject inventoryContent;
    [SerializeField] private TextMeshProUGUI descriptionText; //itemDescription text
    [SerializeField] private GameObject useButton; //item use button if its usable

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
    protected virtual void Start()
    {
        AddInitialItems();
        CreateInventorySlots();
        SetTextAndButton("", false);
    }

    void AddInitialItems()
    {
        foreach (var item in initialList)
        {
            if (!myInventory.ContainsKey(item))
            {
                myInventory[item] = 0;
            }
            if (myInventory[item] + 1 > item.maxNumberHeld)
            {
                continue;
            }

            myInventory[item]++;
        }
    }

    protected void CreateInventorySlots()
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
    protected void ClearInventorySlots()
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
    
    protected void DecreaseItem(InventoryItem item)
    {
        myInventory[item] -= 1;
        if (myInventory[item] == 0)
        {
            myInventory.Remove(item);
        }

        ClearInventorySlots();
        CreateInventorySlots();
    }
}
