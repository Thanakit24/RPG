using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Interactable interactable;
    public InventoryItem item;
    public bool destroyOnPickup = true;
    // Start is called before the first frame update
    private void OnEnable()
    {
        interactable.action += OnPickup;
        interactable.interactMessage = $"Press E to pick up {item.name}";
    }

    private void OnDisable()
    {
        interactable.action -= OnPickup;
    }

    void OnPickup(PlayerController p)
    {
        if (p.invManager.AddItem(item))
        {
            if (destroyOnPickup)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
