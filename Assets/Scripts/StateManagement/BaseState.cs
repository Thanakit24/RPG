using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseState
{
    [HideInInspector] private StateMachine daddy;

    public BaseState (StateMachine daddy)
    {
        this.daddy = daddy;
    }

    public virtual void Start()
    {
    }
    public virtual void Update()
    {

    }
    public virtual void OnExit()
    {

    }

    //public BaseState findState<T>()
    //{
    //    foreach(var state in daddy.allStates)
    //    {
    //        if (state is T) return state;
    //    }
    //    return null;
    //}
}
