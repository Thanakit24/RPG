using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeBossStates
{
    Split,
    Jump,
    Coom
}

public class SlimeBoss : Enemy
{
    private SlimeBossStates attackState = 0;
    public GameObject slimeBossPrefab;
    public int slimeStrength = 20;

    protected override void Update()
    {
        base.Update();
        if (currentState == EnemyStates.AttackPrepare)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                attackCooldown = attackCooldownTimer;
                ChangeStates(EnemyStates.Attack);
                animator.SetInteger("attackEnum", (int) attackState);
                print($"attack state is{attackState}");
                attackState = attackState + 1 % 1;
            }
        }
    }

    public void Coom()
    {
    }

    public void Split()
    {
        if (slimeStrength == 1)
        {
            print("Reached last slime");
            return;
        }
        SlimeBoss newSlime1 = Instantiate(slimeBossPrefab).GetComponent<SlimeBoss>();
        SlimeBoss newSlime2 = Instantiate(slimeBossPrefab).GetComponent<SlimeBoss>();
        newSlime1.slimeStrength = slimeStrength / 2;
        newSlime2.slimeStrength = slimeStrength / 2;
        
        Destroy(gameObject);
    }

    public override void ChangeStates(EnemyStates newState = EnemyStates.IDLE)
    {
        base.ChangeStates(newState);
        switch (newState)
        {
            case EnemyStates.Attack:
                animator.SetBool("Attack", true);
                break;
            default:
                animator.SetBool("Attack", false);
                break;
        }
    }
}