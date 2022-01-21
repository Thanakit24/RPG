using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState currentState;


    protected virtual void Start()
    {
        currentState = new BaseState(this);
    }

    protected virtual void Update()
    {
        currentState.Update();
    }

    protected virtual void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    public virtual void ChangeState(BaseState newState)
    {
        currentState.OnExit();
        newState.OnEnter();
        currentState = newState;
        //newState.Update(); use if 1 frame bug
    }

    public virtual void GotoBase()
    {
        ChangeState(new BaseState(this));
    }
}