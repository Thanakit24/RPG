using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class BaseState
{
    [HideInInspector] public StateMachine daddy;
    public string stateName = "Base State";
    public bool isTimed = false;
    public float age = 0f;

    public BaseState (StateMachine daddy)
    {
        this.daddy = daddy;
        this.stateName = this.GetType().Name;
    }

    public virtual void OnEnter()
    {
       
    }
    public virtual void Update()
    {
        if (!isTimed) return;
        age -= Time.deltaTime;
        if (age <= 0)
        {
            daddy.GotoBase();
        }
    }
    public virtual void FixedUpdate()
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
