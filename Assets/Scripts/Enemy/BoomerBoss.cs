﻿using System.Collections;
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
        if (currentState == EnemyState.AttackPrepare)
        {
            //decremeent
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                attackCooldown = attackCooldownTimer;
                ChangeStates(EnemyState.Attack);

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
        BoomerProjectile projectile = Instantiate(boomerangPrefab, spawnpos.transform.position, Quaternion.identity).GetComponent<BoomerProjectile>();
        projectile.boomBoss = transform;
     
    }

    public override void ChangeStates(EnemyState newState = EnemyState.IDLE)
    {
        base.ChangeStates(newState);
        switch (newState)
        {
            case EnemyState.AttackPrepare:
                break;
        }
    }


    protected override void Move()
    {
        if (!(currentState == EnemyState.Move || currentState == EnemyState.AttackPrepare)) return;
        Vector2 facingDirection = player.transform.position - transform.position;
        rb.MovePosition(rb.position + facingDirection.normalized * moveSpeed * Time.deltaTime); //pathfinding trash
    }
}
