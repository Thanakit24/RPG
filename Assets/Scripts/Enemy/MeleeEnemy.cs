using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    //Attack;
    public BoxCollider2D attackPoint;
    public float attackCooldown;
    public float attackCooldownTimer = 1f;
    public LayerMask playerMask;
  
    protected override void Start()
    {
        base.Start();
        attackPoint.enabled = false;
        attackCooldown = attackCooldownTimer;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update(); //isnt it running the update from enemy first then this update
        if (currentState == EnemyStates.AttackPrepare)
        {
            Attack();
        }
    }
    public void Attack()
    {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown <= 0)
        {
            attackCooldown = attackCooldownTimer;
            currentState = EnemyStates.Attack;
            animator.SetTrigger("Attack");
        }
    }

    //called through animation event
    public void AttackStart()
    {
        attackPoint.enabled = true;
    }

    public void AttackFinish() 
    {
        attackPoint.enabled = false;
        DefaultState();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print("enter collision");
            collision.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        //print("exit collision");
    }
}
