using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoomerAttacks
{
    SingleThrow,
    DoubleThrow
}

public class BoomerBoss : Enemy
{
    private BoomerAttacks attackState = BoomerAttacks.SingleThrow;
    public Transform leftShoot;
    public Transform rightShoot;
    public GameObject boomerangPrefab;

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
    public void InstantiateLeftProjectile() //call through animation events 
    {
        Instantiate(boomerangPrefab, leftShoot.transform.position, Quaternion.identity);
    }

    public void InstantiateRightProjectile()
    {
        Instantiate(boomerangPrefab, rightShoot.transform.position, Quaternion.identity);
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
        Vector2 facingDirection = player.transform.position - transform.position;
        rb.MovePosition(rb.position + facingDirection.normalized * moveSpeed * Time.deltaTime); //pathfinding trash
    }
}
