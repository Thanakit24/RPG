﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Interactable : MonoBehaviour
{
    public TMP_Text interactPrompt;
    public string interactMessage = "Press \"E\" to interact";

    public delegate void InteractAction(PlayerController p);
    public event InteractAction action;

    //public event EventHandler handle;

    private void Start()
    {
        //interactPrompt = GameObject.FindGameObjectWithTag("InteractPrompt").GetComponent<TextMeshProUGUI>();   
        interactPrompt = GameManager.instance.interactText;
    }

    public void Interact(PlayerController p)
    {
        if (action !=null)
        {
            action(p);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactPrompt.gameObject.SetActive(true);
        interactPrompt.text = interactMessage;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactPrompt.gameObject.SetActive(false);
    }
}
