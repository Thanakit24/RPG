using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerController player;
    public PlayerInventory inventory;

    public InventoryItem Obstacle1Item;
    public InventoryItem Obstacle2Item;
    public InventoryItem Obstacle3Item;

    private bool hasItem(InventoryItem item)
    {
        return inventory.myInventory.ContainsKey(item);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Enemy Hit");
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                Vector2 forceDirection = enemy.transform.position - transform.position;
                Vector2 force = forceDirection.normalized * player.knockBack;
                Enemy enemyController = enemy.GetComponent<Enemy>();
                enemyController.TakeDamage(1, player);
                if (enemyController.health == 0)
                    return;
                else
                    enemyController.EnemyDamagedEffect();
                enemyController.KnockBack(force);
            }
        }

        if (hasItem(Obstacle1Item) && collision.gameObject.CompareTag("Obstacle1"))
        {
            Destroy(collision.gameObject);
        }

        if (hasItem(Obstacle2Item) && collision.gameObject.CompareTag("Obstacle2"))
        {
            Destroy(collision.gameObject);
        }

        if (hasItem(Obstacle3Item) && collision.gameObject.CompareTag("Obstacle3"))
        {
            Destroy(collision.gameObject);
        }
    }
}
