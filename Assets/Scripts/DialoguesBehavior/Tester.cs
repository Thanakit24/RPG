using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour

{
    public Conversation convo;
    // Start is called before the first frame update
    public void StartConvo()
    {
        DialogueManager.StartConversation(convo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            StartConvo();
        }
    }
}
