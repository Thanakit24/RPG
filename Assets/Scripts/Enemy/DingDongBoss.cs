using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DingDongAttack
{
    SingleThrow,
    DoubleThrow
}

public class DingDongBoss : Enemy
{
    private BoomerAttacks attackState = BoomerAttacks.SingleThrow;
    public Transform leftShoot;
    public Transform rightShoot;
    public GameObject boomerangPrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //TODO put in enemy
        if (currentState == EnemyStates.AttackPrepare)
        {
            //decremeent
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                attackCooldown = attackCooldownTimer;
                ChangeStates(EnemyStates.Attack);

                //Decide Attack
                if (attackState == BoomerAttacks.SingleThrow)
                {
                    animator.SetTrigger("SingleThrow");
                }
                else
                {
                    animator.SetTrigger("DoubleThrow");
                }

                //Decide new attack
                print($"attack state is{attackState}");
                attackState = attackState + 1 % 1;
            }
        }
    }

    public void InstantiateProjectile(int isLeft) //call through animation events 
    {
        Transform spawnpos = rightShoot;
        if (isLeft == 0)
        {
            spawnpos = leftShoot;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        Boomerang boomerang = Instantiate(boomerangPrefab, spawnpos.transform.position, Quaternion.Euler(0, 0, angle)).GetComponent<Boomerang>();
        boomerang.boss = this;
        boomerang.initialDirection = direction;

    }

    public override void ChangeStates(EnemyStates newState = EnemyStates.IDLE)
    {
        base.ChangeStates(newState);
        switch (newState)
        {
            case EnemyStates.AttackPrepare:
                //Decide new attack
                //attackState = attackState + 1 % 2;
                break;
        }
    }


    protected override void Move()
    {
        if (!(currentState == EnemyStates.Move  || currentState == EnemyStates.AttackPrepare)) return;
        Vector2 facingDirection = player.transform.position - transform.position;
        rb.MovePosition(rb.position + facingDirection.normalized * moveSpeed * Time.deltaTime); //pathfinding trash
    }
}
