using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleProjectile : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void OpenPuddleCollider() //call through animation events
    {
        boxCollider.enabled = true;
    }

    public void ClosePuddleCollider() //call through animation events 
    {
        boxCollider.enabled = false; //probably dont need this since im destroying it anw
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(2);
        }
    }
}

