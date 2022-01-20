using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class Player : StateMachine
{
    //public Queue<BaseState> bufferedStates = new Queue<BaseState>();
    public BaseState bufferedState;
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 moveDir;
    public Vector2 lastDir;

    #region Movement Variables
    public float moveSpeed = 5f;
    public float initialDashSpeed = 10f;
    public float endDashSpeed = 2.5f;
    public float dashDur = 0.75f;
    #endregion

    #region Melee Attack Variables
    public float atkDur = 0.2f;
    public int atkSeq = 0;
    public float[] attSeqTimes = {0.5f, .5f, 1f};

    public float chargeDurMax = 1f;
    #endregion

    #region Animation Keys
    public static readonly int HorizontalKey = Animator.StringToHash("Horizontal");
    public static readonly int VerticalKey = Animator.StringToHash("Vertical");
    public static readonly int MovingKey = Animator.StringToHash("Moving");
    public static readonly int DashKey = Animator.StringToHash("Dash");
    public static readonly int LightAtkKey = Animator.StringToHash("LightAtk");
    public static readonly int AtkSeqKey = Animator.StringToHash("AtkSeq");
    public static readonly int HeavyAtkKey = Animator.StringToHash("HeavyAtk");
    public static readonly int AtkChargeKey = Animator.StringToHash("AtkCharge");
    public static readonly int RollAtkKey = Animator.StringToHash("RollAtk");
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        currentState = new Idle(this);
    }

    protected override void Update()
    {
        base.Update();
        anim.SetFloat(HorizontalKey, lastDir.x);
        anim.SetFloat(VerticalKey, lastDir.y);
        if (currentState is Idle)
            anim.SetBool(MovingKey, false);
        else if (currentState is Move)
            anim.SetBool(MovingKey, true);
        //if (Input.GetButtonDown("Dash"))
        //{
        //    ChangeState(new Dash(this));
        //}

    }
 
    private void OnTriggerEnter(Collider other)
    {

        if (currentState is Idle)
        {

        }
    }

    public override void GotoBase()
    {
        //if (bufferedStates.Count != 0)
        //{
        //    ChangeState(bufferedStates.Dequeue());
        //    bufferedStates = null;
        //}
        if (bufferedState != null)
        {
            ChangeState(bufferedState);
            bufferedState = null;
        }
        else
        {
            ChangeState(new Idle(this));
        }
    }
}
