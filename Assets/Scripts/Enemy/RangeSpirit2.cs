using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSpirit2 : Enemy
{
    public float instantiateCooldown;
    private float instantiateCooldownTimer = 2.5f;
    public GameObject puddleProjectile;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        instantiateCooldown = instantiateCooldownTimer;
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (currentState == EnemyStates.AttackPrepare)
        {
            PreparePuddle();
        }
    }
    public void PreparePuddle()
    {
        if (instantiateCooldown > 0)
        {
            instantiateCooldown -= Time.deltaTime;
        }
        else
        {
            animator.SetTrigger("Attack");
            currentState = EnemyStates.Attack;
        }
    }
    public void InstantiatePuddle() //call through animation events 
    {
        Instantiate(puddleProjectile, target.position, Quaternion.identity);
        instantiateCooldown = instantiateCooldownTimer;
        currentState = EnemyStates.IDLE; 
    }

}
