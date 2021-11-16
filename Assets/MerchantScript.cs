using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantScript : MonoBehaviour
{
    public Interactable interactable;
    // Start is called before the first frame update
    
    void Start()
    {
        interactable.action += OnShopInteract;
    }

    void OnShopInteract(PlayerController p)
    {
        print("Irashaimasen ka, honto ni subarashi sekai ne your mom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
