using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class BaseEnemy : ActorBase
{
    public BaseState bufferedState;
    [HideInInspector] public Rigidbody2D rb;
    public Vector2 moveDir;
    public Vector2 aimDir;

    [HideInInspector] public Vector2 target;

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
        UnitManager.Instance.Enemies.Add(this);
        currentState = new ChasePlayer(this);
    }

    public void Despawn()
    {
        UnitManager.Instance.Enemies.Remove(this);
        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();
        FindTarget();
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

    private void FindTarget()
    {
        float minDist = float.MaxValue;
        foreach (Player unit in UnitManager.Instance.Players)
        {
            if (unit != null && Vector2.SqrMagnitude(transform.position - unit.transform.position) < minDist)
            {
                minDist = Vector2.SqrMagnitude(transform.position - unit.transform.position);
                target = unit.transform.position;
            }
        }
    }
}
