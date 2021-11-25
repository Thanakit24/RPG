using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public Transform target;
    public BoomerBoss boomBoss;
    Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        //moveDirection = (target.transform.position - transform.position).normalized * speed;
        //rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    private void Update()
    {
        if (transform.position == target.transform.position)
        {
            moveDirection = (transform.position - target.transform.position.normalized) * speed;
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
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

