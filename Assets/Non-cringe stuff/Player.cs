using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerStates;

public class Player : ActorBase
{
    //public Queue<BaseState> bufferedStates = new Queue<BaseState>();
    public BaseState bufferedState;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    public Vector2 moveDir;
    public Vector2 lastDir;
    public Vector2 aimDir;
    public Transform weapon;

    public StateMachine sm;

    public float maxIframeDur = 0.5f;
    public float iframeDur = 0f;
    // public bool isIframe = false;

    #region Dash Variables
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

    #region UI
    [Header("UI")]
    public Image[] hearts;
    public Sprite filledHeart;
    public Sprite emptyHeart;
    public SpriteRenderer sr;
    public SpriteRenderer swordSr;
    #endregion

    #region Inventory
    [Header("Pickups")]
    public PlayerInventory invManager;
    public GameObject invGO;
    public float interactRange;
    public LayerMask interactLayerMask;
    #endregion

    #region Animation Keys
    public static readonly int HorizontalKey = Animator.StringToHash("Horizontal");
    public static readonly int VerticalKey = Animator.StringToHash("Vertical");
    public static readonly int MoveKey = Animator.StringToHash("Move");
    public static readonly int DashAnimKey = Animator.StringToHash("Roll");
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
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        UnitManager.Instance.Players.Add(this);
        currentState = new Idle(this);
    }

    public void Despawn()
    {
        UnitManager.Instance.Players.Remove(this);
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();

        iframeDur -= Time.deltaTime;
        if (iframeDur > 0)
        {
            sr.color = new Color(Random.value, Random.value, Random.value);
        }
        else
        {
            sr.color = Color.white;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        print("hey man");
        if (iframeDur > 0)
        {
            return;
        }
        iframeDur = maxIframeDur;
        print("OUCH FUCK");

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