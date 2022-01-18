using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    public Vector2 input;
    public Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        currentState = new PlayerStates.Idle(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (currentState is PlayerStates.Idle)
        {
            
        }
    }
}
