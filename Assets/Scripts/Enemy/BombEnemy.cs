using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemy
{
    public BoxCollider2D triggerCollider;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (currentState == EnemyStates.AttackPrepare)
        {
            Explode();
        }
    }
    // Update is called once per frame
    public void Explode()
    {
        currentState = EnemyStates.Attack;
        animator.SetTrigger("Attack");
    }
    public void OpenExplodeCollider()
    {
        //print("Called exploder colldier");
        triggerCollider.enabled = true; 
    }

    public void CloseExplodeCollider()
    {
        //print("Close collider");
        triggerCollider.enabled = false;
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print("enter collision");
            collision.GetComponent<PlayerController>().TakeDamage(3);
        }
    }
}
