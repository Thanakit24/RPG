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
        Invoke("TeleportToScene", 0.2f);
    }

    private void OnEnable()
    {
        interactable.action += StartTeleport;
    }

    private void OnDisable()
    {
        interactable.action -= StartTeleport;
    }
}

