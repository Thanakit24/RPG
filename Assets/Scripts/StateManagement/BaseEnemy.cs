using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class BaseEnemy : ActorBase
{
    public BaseState bufferedState;
    public Rigidbody2D rb;
    public Transform player;
    public Vector2 currentPos;
    public Vector2 playerPos;
    public Vector2 moveDir;
    public Vector2 targetDir;

    #region Attack Variables
    [Header("Attack")]
    public float atkCooldown = 3f;
    public float cooldownTimer;
    public float atkRange = 3f;
    public float windupDur = 1.5f;
    public float atkDur = 0.5f;
    public float windupSpeed = 2f;
    public float initialDashSpeed = 10f;
    public float endDashSpeed = 2.5f;
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        base.Start();
        currentState = new ChasePlayer(this);
    }
    protected override void Update()
    {
        base.Update();
        playerPos = player.position;
        currentPos = transform.position;
    }
    public override void GotoBase()
    {
        if (bufferedState != null)
        {
            ChangeState(bufferedState);
            bufferedState = null;
        }
        else
            ChangeState(new ChasePlayer(this));
    }
}
