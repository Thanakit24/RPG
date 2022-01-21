using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerStates;

public class Player : StateMachine
{
    //public Queue<BaseState> bufferedStates = new Queue<BaseState>();
    public BaseState bufferedState;
    public Rigidbody2D rb;
    public Animator anim;
    public Vector2 moveDir;
    public Vector2 lastDir;
    public Vector2 aimDir;
    public Transform weapon;

    #region Movement Variables
    public float moveSpeed = 5f;
    public float initialDashSpeed = 10f;
    public float endDashSpeed = 2.5f;
    public float dashDur = 0.75f;
    #endregion

    #region Melee Attack Variables
    [Header("Melee Attack")]
    public float atkDur = 0.2f;
    public int atkSeq = 0;
    public float[] attSeqTimes = { 1f, 1f, 2f };

    public float chargeDurMax = 1f;

    public int attackDamage = 1;
    public float knockBack;
    public LayerMask enemyMask;
    #endregion

    #region Health
    [Header("Health")]
    public int currentHealth;
    public int maxHealth;
    public Image[] hearts;
    public Sprite filledHeart;
    public Sprite emptyHeart;
    public float iframeDur = 1;
    public SpriteRenderer sr;
    public SpriteRenderer swordSr;
    public float flashFreq = 0.2f;
    public Color flashColor;
    public Color regularColor;
    #endregion

    #region Animation Keys
    public static readonly int HorizontalKey = Animator.StringToHash("Horizontal");
    public static readonly int VerticalKey = Animator.StringToHash("Vertical");
    public static readonly int MoveKey = Animator.StringToHash("Move");
    public static readonly int DashKey = Animator.StringToHash("Dash");
    public static readonly int LightAtkKey = Animator.StringToHash("LightAtk");
    public static readonly int AtkSeqKey = Animator.StringToHash("AtkSeq");
    public static readonly int HeavyAtkKey = Animator.StringToHash("HeavyAtk");
    public static readonly int AtkChargeKey = Animator.StringToHash("AtkCharge");
    public static readonly int DashAtkKey = Animator.StringToHash("DashAtk");
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