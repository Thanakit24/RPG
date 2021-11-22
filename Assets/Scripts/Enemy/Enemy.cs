using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum EnemyStates
{
    IDLE,
    Move,
    Knocked,
    AttackPrepare,
    Attack,
}
public class Enemy : MonoBehaviour
{
    public EnemyStates currentState;
    protected Rigidbody2D rb; //protected is private but for parents and children is accessible
    public int health;
    public float moveSpeed;
    public int enemyCurrency; //currency; 

    //Attack
    public string enemyName;
    public int attackDamage;
    public float attackRange = 10f;
    public float knockBack = 2.5f;

    //Take Damage Effect
    protected Animator animator;
    private SpriteRenderer enemySprite;
    public int numberOfFlashes;
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public float knockbackDuration = 0.5f;

    public Transform target;
    protected Vector3 facingDirection;
    public bool facingRight;

    //Enemy Pathfinding
    private Seeker seeker;
    private Path path;
    private int currentWaypoint;
    public float nextWaypointDistance = 3f;
    private bool reachedEndOfPath = false;
    public float movementIntervalMax = 3f;
    private float movementInterval = 0.3f;
    private Vector2 randMovedir;
    public float drunkenness = 0.5f;

    public float maxAnimStuck = 4f;
    private float animStuck = 4f;

    //Animation
    public string idleAnim;
    private int idleHash;

    //Event
    public delegate void OnEnemyDeath(Enemy e);
    public event OnEnemyDeath deathEvent;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        currentState = EnemyStates.IDLE;
        animator = GetComponent<Animator>();
        idleHash = Animator.StringToHash(idleAnim);
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, 0.1f);
        animStuck = maxAnimStuck;

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 1;
        }
    }
    protected virtual void Update()
    {
        enemySprite.sortingOrder = Mathf.FloorToInt(transform.position.y * -100);

        //MAX TIME STUCK

        //DONT DO THIS IN A NORMAL UPDATE< INFACT DONT DO THIS 
        if (currentState == EnemyStates.Attack || currentState == EnemyStates.Knocked)
        {
            if (animStuck > 0)
            {
                animStuck -= Time.deltaTime;
                return;
            }
            else
            {
                Debug.Log("CRINGE ALERT ANIM WAS STUCK");
                currentState = EnemyStates.IDLE;
                animStuck = maxAnimStuck;
            }
        }

        animStuck = maxAnimStuck;
        facingDirection = Vector2.zero;
        if (Vector3.Distance(target.position, transform.position) <= attackRange)
        {
            ChangeStates(EnemyStates.AttackPrepare);
        }
        else
        {
            ChangeStates(EnemyStates.Move);
            facingDirection = target.position - transform.position;
            facingDirection = facingDirection.normalized;
        }

        if (target.transform.position.x < transform.position.x && facingRight)
        {
            Flip();
        }

        else if (target.transform.position.x > transform.position.x  && !facingRight)
        {
            Flip();
        }
    }


    private void FixedUpdate()
    {
        if (currentState == EnemyStates.Move)
        {
            Move();
        }
    }

    private void Move()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        if (movementInterval > 0)
        {
            movementInterval -= Time.deltaTime;
        }
        else
        {
            randMovedir = Random.insideUnitCircle;
            movementInterval = Random.Range(0, movementIntervalMax);
        }


        Vector2 direction = (((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized + randMovedir * drunkenness).normalized;
        Vector2 relativePos = direction * moveSpeed * Time.deltaTime;

        Debug.DrawLine(rb.position, rb.position + relativePos, Color.black, 99999f);
        //rb.AddForce(newPos);
        rb.MovePosition(rb.position + relativePos);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //rb.MovePosition(rb.position + (Vector2)facingDirection * moveSpeed * Time.deltaTime); //pathfinding trash
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            GiveCurrency();
            Dead();
        }
    }
    public void KnockBack(Vector2 force)
    {
        currentState = EnemyStates.Knocked;
        StartCoroutine(Knocking(force));
    }

    private IEnumerator Knocking(Vector2 force)
    {
        rb.velocity = force;
        //animator.Play(idleHash);
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        currentState = EnemyStates.IDLE;
    }
    public void EnemyDamagedEffect()
    {
        StartCoroutine(FlashSprite());
    }
    private IEnumerator FlashSprite()
    {
        int temp = 0;
        while (temp < numberOfFlashes)
        {
            enemySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            enemySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
    }
    public void GiveCurrency()
    {
        PlayerCurrency.instance.GainCurrecy(enemyCurrency);
    }

    private void Dead()
    {
        deathEvent?.Invoke(this);
        animator.SetTrigger("Dead");
    }

    private void RemoveFromScene() //call through animation event 
    {
        Destroy(gameObject);
    }

    public void DefaultState()
    {
        if (currentState == EnemyStates.Knocked) return;
        ChangeStates();
    }

    public void ChangeStates(EnemyStates newState = EnemyStates.IDLE)
    {
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
}
