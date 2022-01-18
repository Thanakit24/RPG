using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;

    public BaseState[] allStates;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.OnExit();
        currentState = newState;
    }
}
