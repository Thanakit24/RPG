﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    Default,
    attack,
    invulnerable,
    interact,

}
public class PlayerController : MonoBehaviour
{
    //Movement
    [Header("Move")]
    public float moveSpeed;
    private Rigidbody2D rb;
    public bool isMoving = false;
    private Vector2 moveDirection; //changing sprite direction
    private Vector2 lastDirection;
    public PlayerState currentState;
    private Animator animator;

    //Attack
    [Header("Attack")]
    public GameObject attackPoint;
    public float attackCooldown;
    public float attackCooldownTimer = 1f;
    public int attackDamage = 1;
    public float knockBack;
    public LayerMask enemyMask;
    public bool cantAttack = false;
   
    //Health
    [Header("Health")]
    public int health;
    public int MaxOfHearts;
    public Image[] hearts;
    public Sprite fullContainer;
    public Sprite emptyContainer;
    private bool isInvulnerable;
    public float invulnerableDurationTimer = 1;
    private float invulnerableDuration;
    public LayerMask playerLayer;
    public SpriteRenderer mySprite;
    public int numberOfFlashes;
    public float flashDuration;
    public Color flashColor;
    public Color regularColor;

    [Header("Pickups")]
    public InventoryItem sample;
    public InventoryManager invManager;

    //public PlayerCurrency playerCurrency;


    //Animation States
    void Start()
    {
        isInvulnerable = false;
        attackPoint.SetActive(false);
        currentState = PlayerState.Default;
        attackCooldown = attackCooldownTimer;
        invulnerableDuration = invulnerableDurationTimer;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerHealth();
        ProcessInputs();

        if (isInvulnerable)
        {
            currentState = PlayerState.invulnerable;
            invulnerableDuration -= Time.deltaTime; 
            if (invulnerableDuration <= 0)
            {
                isInvulnerable = false;
                currentState = PlayerState.Default;
                invulnerableDuration = invulnerableDurationTimer;
    
            }
        }

    }
    private void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        moveDirection = Vector2.zero;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (moveDirection != Vector2.zero)
        {
            lastDirection = moveDirection;
        }

        if (cantAttack)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                cantAttack = false;
                attackCooldown = attackCooldownTimer;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && !cantAttack)
        {
            Attack();
        }
        else if (currentState == PlayerState.Default || currentState == PlayerState.invulnerable)
        {
            UpdateMoveAnimations();
        }

        //SAMPLE DEBUGGING CODE
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            invManager.IncreaseItem(sample);
            print("Brain");
        }
    }

    #region Move
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void UpdateMoveAnimations()
    {
        if (moveDirection != Vector2.zero)  //saves last anim frame
        {
            animator.SetFloat("Horizontal", moveDirection.x);
            animator.SetFloat("Vertical", moveDirection.y);
            animator.SetFloat("Magnitude", moveDirection.magnitude);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    #endregion

    #region AttackBehavior
    void Attack()
    {
        float xdot = Vector2.Dot(lastDirection, Vector2.right);
        float ydot = Vector2.Dot(lastDirection, Vector2.up);
        float angle = Mathf.Abs(xdot) > Mathf.Abs(ydot) ? xdot > 0 ? 90 : 270 : ydot > 0 ? 180 : 0; //bruh wtf
        attackPoint.transform.rotation = Quaternion.Euler(0, 0, angle);
        print(angle);

        cantAttack = true;
        animator.SetTrigger("Attacking");
        attackPoint.SetActive(true);
    }

    void AttackFinish() //called through an animation event on player
    {
        attackPoint.SetActive(false);
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
                Vector2 force = forceDirection.normalized * knockBack;
                Enemy enemyController = enemy.GetComponent<Enemy>();
                enemyController.TakeDamage(1);
                if (enemyController.health == 0)
                    return;
                    else
                enemyController.EnemyDamagedEffect();
                enemyController.KnockBack(force);

            }
        }
    }
    #endregion 

    #region SAVE & LOAD DATA
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position; 

    }

    #endregion 

    private void PlayerHealth()
    {
        if (health > MaxOfHearts)
        {
            health = MaxOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++) //heart sprites more than i
        {
            if (i < health)
            {
                hearts[i].sprite = fullContainer; 
            }
            else
            {
                hearts[i].sprite = emptyContainer;
            }

            if (i < MaxOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
   
    public void TakeDamage(int damage)
    {
        if (currentState ==  PlayerState.invulnerable)
        {
            return;
        }
        else
        {
            health -= damage;
            isInvulnerable = true;
            StartCoroutine(FlashDamage());
            if (health <= 0)
            {
                //call game manager death
            }
        }
    }
    private IEnumerator FlashDamage()
    {
        int temp = 0;
        while (temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++; 
        }
    }
}
