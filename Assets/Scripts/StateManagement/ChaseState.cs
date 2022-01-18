using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    [SerializeField] private float speed;

    public ChaseState(StateMachine daddy) : base(daddy)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
    public override void Update()
    {
        base.Update();
    }
}
