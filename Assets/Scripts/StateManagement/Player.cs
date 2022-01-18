using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class Player : StateMachine
{
    public Vector2 moveDir;
    public Vector2 lastDir;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float dashSpeed = 20f;
    public float dashDur = 0.75f;

    public float chargeDur = 0.5f;


    public BaseState bufferedState;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        currentState = new Idle(this);
    }

    protected override void Update()
    {
        //if (Player.currentState is Attack)
        //{
        //    base.Update();
        //}
        //moveDir = Input

        

        base.Update();
        if (Input.GetButtonDown("Dash"))
        {
            ChangeState(new Dash(this));
        }

    }
 
    private void OnTriggerEnter(Collider other)
    {

        if (currentState is Idle)
        {

        }
    }

    public override void GotoBase()
    {
        if (bufferedState != null)
        {
            ChangeState(new Idle(this));
        }
        else
        {
            ChangeState(bufferedState);
        }
    }

}
