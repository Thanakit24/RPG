using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum EnemyState
{
    IDLE,
    Move,
    Knocked,
    AttackPrepare,
    Attack,
    Dead,
}
public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    protected Rigidbody2D rb; //protected is private but for parents and children is accessible
    public int health = 5;
    public float moveSpeed = 2f;
    public int enemyCurrency = 10; //currency; 

    [Header("Attack")]
    public string enemyName;
    public int attackDamage;
    public float knockBack = 2.5f;
    public int collisionDamage = 1;
    public float attackCooldown;
    public float attackCooldownTimer = 1f;

    [Header("State Conditions & Patrol")]
    public bool isPatrol = false;
    public float patroltWaypointDistance = 2f;
    public Transform[] patrolPoints;
    public int patrolIndex;
    public float waitTime = 0.5f;
    private bool waitAtPatrol = false;
    public float chaseRadius = 1f;
    public float attackRange = 10f;

    [Header("Take Damage Effect")]
    protected Animator animator;
    private SpriteRenderer enemySprite;
    public int numberOfFlashes;
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public float knockbackDuration = 0.5f;

    private Transform target;
    public Transform player;
    public bool facingRight;

    [Header("Enemy Pathfinding")]
    public bool isPathfinding = true;
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
        player = GameManager.instance.player.transform;
        target = player;
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        currentState = EnemyState.IDLE;
        animator = GetComponent<Animator>();
        idleHash = Animator.StringToHash(idleAnim);
        if (isPathfinding)
        {
            seeker = GetComponent<Seeker>();
            InvokeRepeating("UpdatePath", 0f, 0.1f);
        }
        animStuck = maxAnimStuck;
        attackCooldown = attackCooldownTimer;
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
        if (currentState == EnemyState.Dead)
            return;
        //MAX TIME STUCK

        //DONT DO THIS IN A NORMAL UPDATE< INFACT DONT DO THIS 
        if (currentState == EnemyState.Attack || currentState == EnemyState.Knocked)
        {
            if (animStuck > 0)
            {
                animStuck -= Time.deltaTime;
                return;
            }
            else
            {
                Debug.Log("CRINGE ALERT ANIM WAS STUCK");
                currentState = EnemyState.IDLE;
                animStuck = maxAnimStuck;
            }
        }

        animStuck = maxAnimStuck;
        //facingDirection = Vector2.zero;
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (isPatrol && distanceFromPlayer > chaseRadius)
        {
            Patrol();
        }
        else if (distanceFromPlayer <= attackRange)
        {
            //attack radius
            waitAtPatrol = false;
            ChangeStates(EnemyState.AttackPrepare);
        }
        else
        {
            //chase radius
            target = player;
            waitAtPatrol = false;
            ChangeStates(EnemyState.Move);
        }

        if (target.transform.position.x < transform.position.x && facingRight)
        {
            if (!waitAtPatrol || !isPatrol)
                Flip();
        }

        else if (target.transform.position.x > transform.position.x && !facingRight)
        {
            if (!waitAtPatrol || !isPatrol)
                Flip();
        }
    }

    private void Patrol()
    {
        ChangeStates(EnemyState.Move);
        target = patrolPoints[patrolIndex];
        if (!waitAtPatrol && Vector3.Distance(transform.position, patrolPoints[patrolIndex].position) < patroltWaypointDistance)
        {
            waitAtPatrol = true;
            patrolIndex++;
            patrolIndex = patrolIndex % patrolPoints.Length;//modulo: remainder used to wrap to 0
            StartCoroutine(WaitAtPatrol(waitTime));
        }
    }

    IEnumerator WaitAtPatrol(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        waitAtPatrol = false;
    }

    private void FixedUpdate()
    {
        Move();

    }

    protected virtual void Move()
    {
        if (currentState != EnemyState.Move) return;
        if (path == null || waitAtPatrol) return;
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


    public void TakeDamage(int damage, PlayerController player)
    {
        health -= damage;
        if (health <= 0)
        {
            
            Dead(player);
        }
    }
    public void KnockBack(Vector2 force)
    {
        currentState = EnemyState.Knocked;
        StartCoroutine(Knocking(force));
    }

    private IEnumerator Knocking(Vector2 force)
    {
        rb.velocity = force;
        //animator.Play(idleHash);
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        currentState = EnemyState.IDLE;
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
    private void Dead(PlayerController player)
    {
        rb.velocity = Vector2.zero;
        ChangeStates(EnemyState.Dead);
        player.invManager.GainCurrecy(enemyCurrency);
        animator.SetTrigger("Dead");
        deathEvent?.Invoke(this);
    }

    private void RemoveFromScene() //call through animation event 
    {
        Destroy(gameObject);
    }

    public void DefaultState()
    {
        if (currentState == EnemyState.Knocked) return;
        ChangeStates();
    }

    public virtual void ChangeStates(EnemyState newState = EnemyState.IDLE)
    {
        currentState = newState;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().TakeDamage(collisionDamage);
            
        }
    }

    private void OnDrawGizmos()
    {
        KongrooUtils.DrawGizmoCircle(transform.position, chaseRadius, Color.yellow);
        KongrooUtils.DrawGizmoCircle(transform.position, attackRange, Color.red);
    }

    //im gonna go get some air for 5-10 mins, head start to hurt brb
}
