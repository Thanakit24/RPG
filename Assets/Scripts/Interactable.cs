using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Interactable : MonoBehaviour
{
    public bool isTutorial;
    public TMP_Text interactPrompt;
    public string interactMessage = "Press \"E\" to interact";

    public delegate void InteractAction(PlayerController p);
    public event InteractAction action;

    private bool interacting = false;

    //public event EventHandler handle;

    private void Start()
    {
        //interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<TextMeshProUGUI>();   
        interactPrompt = GameManager.instance.interactText;
    }

    public void Interact(PlayerController p)
    {
        if (interacting) return;
        if (action !=null)
        {
            action(p);
        }
        interactPrompt.gameObject.SetActive(false);
        interacting = true;
    }

    public void FinishInteracting()
    {
        interacting = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (interactFinished) return;
        interactPrompt.gameObject.SetActive(true);
        interactPrompt.text = interactMessage;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactPrompt.gameObject.SetActive(false);
        if (isTutorial)
        {
            Destroy(gameObject);
        }
    }

}
