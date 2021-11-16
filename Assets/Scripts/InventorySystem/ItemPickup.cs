using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Interactable interactable;
    public InventoryItem item;
    // Start is called before the first frame update
    private void OnEnable()
    {
        interactable.action += OnPickup;
    }

    private void OnDisable()
    {
        interactable.action -= OnPickup;
    }

    void OnPickup(PlayerController p)
    {
        if (p.invManager.AddItem(item))
        {
            Destroy(this.gameObject);
        }
    }

}
