using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpirit : Enemy
{
    public Transform shootPoint;
    public float shootCooldown;
    private float shootCooldownTimer = 3f;
    public GameObject projectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        shootCooldown = shootCooldownTimer;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (currentState == EnemyStates.AttackPrepare )
        {
            ShootProjectile();
        }

    }
   
    private void ShootProjectile()
    {
        if (shootCooldown <= 0)
        {
            animator.SetTrigger("Attack");
            currentState = EnemyStates.Attack;
        }
        else
        {
            shootCooldown -= Time.deltaTime;
        }
    }
    public void InstantiateProjectile() //call through animation events
    {
        Instantiate(projectile, shootPoint.position, Quaternion.identity);
        //Debug.Log("Instantiate & Called");
        //Debug.Log("Shoot projectile");
        shootCooldown = shootCooldownTimer;
        currentState = EnemyStates.IDLE;
    }
   
}
