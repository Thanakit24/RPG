using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public float initialSpeed = 2f;
    public float decceleration = 1f;
    public float existDuration = 3f;
    public Vector2 initialDirection;
    public DingDongBoss boss;
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = initialDirection * initialSpeed;
        Invoke("Die", existDuration);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.velocity += -initialDirection * decceleration * Time.deltaTime;
    }
}
