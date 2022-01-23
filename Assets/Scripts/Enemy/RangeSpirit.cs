using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpirit : Enemy
{
    public Transform shootPoint;

    public GameObject projectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (currentState == EnemyState.AttackPrepare )
        {
            ShootProjectile();
        }

    }
   
    private void ShootProjectile()
    {
        if (attackCooldown <= 0)
        {
            animator.SetTrigger("Attack");
            currentState = EnemyState.Attack;
        }
        else
        {
            attackCooldown -= Time.deltaTime;
        }
    }
    public void InstantiateProjectile() //call through animation events
    {
        Instantiate(projectile, shootPoint.position, Quaternion.identity);
        //Debug.Log("Instantiate & Called");
        //Debug.Log("Shoot projectile");
        attackCooldown = attackCooldownTimer;
        currentState = EnemyState.IDLE;
    }
   
}
