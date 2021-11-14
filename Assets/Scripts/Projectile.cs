using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Transform target;
    Vector2 moveDirection;
 
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(this.gameObject, 4);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(1);
            Destroy(gameObject);
            //Deal damage
            //Destroy object
        }
        if (collision.gameObject.CompareTag("Invulnerable") || collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

       
    }

}
