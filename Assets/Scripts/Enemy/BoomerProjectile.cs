using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerProjectile : MonoBehaviour
{
    public float speed;
    public float travelTime;
    public float travelMaxTime;
    private Rigidbody2D rb;
    private Transform target;
    public Transform boomBoss;
    Vector2 moveDirection;
    Vector2 newDirection;
    // Start is called before the first frame update
    void Start()
    {
        travelTime = travelMaxTime;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //boomBoss = GameObject.FindGameObjectWithTag("BoomerBoss").transform;
        rb = GetComponent<Rigidbody2D>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void Update()
    {
        travelTime -= Time.deltaTime;
        if (travelTime < 0)
        {
            newDirection = Vector2.MoveTowards(transform.position, boomBoss.position, speed * Time.deltaTime);
            rb.velocity = new Vector2(newDirection.x, newDirection.y);
        }
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
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }


}

