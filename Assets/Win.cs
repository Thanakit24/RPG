using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public Conversation convo;
    public Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WinFunc(PlayerController p)
    {
        DialogueManager.StartConversation(convo);
        DialogueManager.textEnd += Winmore;
    }

    public void Winmore()
    {
        SceneManager.LoadScene("Credits");
    }

    private void OnEnable()
    {
        interactable.action += WinFunc;
    }

    private void OnDisable()
    {
        interactable.action -= WinFunc;
    }
}
