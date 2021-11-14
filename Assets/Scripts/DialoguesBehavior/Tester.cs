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
}
