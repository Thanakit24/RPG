using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Interactable interactable;
    public int transitionToLoadOn;
    public string sceneToLoad;

    void TeleportToScene(PlayerController p)
    {
        SceneScript.instance.LoadScene(sceneToLoad, transitionToLoadOn);
        
    }
    private void OnEnable()
    {
        interactable.action += TeleportToScene;
    }

    private void OnDisable()
    {
        interactable.action -= TeleportToScene;
    }
}

