using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Interactable interactable;
    public int transitionToLoadOn;
    public string sceneToLoad;

    void TeleportToScene()
    {
        print("tping");
        SceneScript.instance.LoadScene(sceneToLoad, transitionToLoadOn);
    }
    void StartTeleport(PlayerController p)
    {
        // Invoke("TeleportToScene", 0.2f);
        TeleportToScene();
    }

    private void Start()
    {
        interactable.action += StartTeleport;
    }

    private void OnDisable()
    {
        interactable.action -= StartTeleport;
    }
}

