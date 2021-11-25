using System.Collections;
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
    Dash

}
public class PlayerController : MonoBehaviour
{
    //Movement
    [Header("Move")]
    public float moveSpeed;
    public float dashSpeed;

    private float dashTime;
    public float dashTimeMax = 0.5f;

    private float dashCooldown;
    public float dashCoolDownMax = 2f;

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
    public float invulnerableDurationTimer = 1;
    private float invulnerableDuration;
    public SpriteRenderer sr;
    public int numberOfFlashes;
    public float flashDuration;
    public Color flashColor;
    public Color regularColor;

    [Header("Pickups")]
    public InventoryItem sample;
    public PlayerInventory invManager;
    public GameObject invGO;
    //public ContactFilter2D interactFilter;
    //public Collider2D interactCol;
    public float interactRange;
    public LayerMask interactLayerMask;


    //public PlayerCurrency playerCurrency;


    //Animation States
    void Start()
    {
        attackPoint.SetActive(false);
        currentState = PlayerState.Default;
        attackCooldown = attackCooldownTimer;
        dashTime = dashTimeMax;
        dashCooldown = dashCoolDownMax;
        invulnerableDuration = invulnerableDurationTimer;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        sr.sortingOrder = Mathf.FloorToInt(transform.position.y * -100);
        PlayerHealth();
        ProcessInputs();

        switch (currentState)
        {
            case PlayerState.Dash:
                dashTime -= Time.deltaTime;
                dashCooldown = dashCoolDownMax;
                if (dashTime <= 0)
                {
                    currentState = PlayerState.Default;
                    dashTime = dashTimeMax;
                }
                break;

            case PlayerState.invulnerable:
                invulnerableDuration -= Time.deltaTime;
                if (invulnerableDuration <= 0)
                {
                    currentState = PlayerState.Default;
                    invulnerableDuration = invulnerableDurationTimer;
                }
                break;
            default:
                dashCooldown -= Time.deltaTime;
                break;
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
            Scene currentScene = SceneManager.GetActiveScene();
            SceneScript.instance.loadedScenes.Remove(currentScene.buildIndex);
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldown < 0 && currentState != PlayerState.Dash)
        {
            currentState = PlayerState.Dash;
            animator.SetTrigger("Dash");

            //dash torugh enemy
            //invulnerability    
            //disable gap collider
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, interactRange, interactLayerMask);
            if (col)
            {
                col.GetComponent<Interactable>().Interact(this);
            }
        }

        //Toggle inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            invGO.SetActive(!invGO.activeSelf);
            if (invGO.activeSelf)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && invGO.activeSelf)
        {
            invGO.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    #region Move
    void Move()
    {
        if (currentState == PlayerState.Dash)
        {
            rb.velocity = new Vector2(lastDirection.x * dashSpeed, lastDirection.y * dashSpeed);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
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
        //print(angle);

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
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BoomerBoss"))
        {
            //Debug.Log("Enemy Hit");
            Rigidbody2D enemy = collision.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                Vector2 forceDirection = enemy.transform.position - transform.position;
                Vector2 force = forceDirection.normalized * knockBack;
                Enemy enemyController = enemy.GetComponent<Enemy>();
                enemyController.TakeDamage(1, this);
                if (enemyController.health == 0)
                    return;
                else
                    enemyController.EnemyDamagedEffect();
                enemyController.KnockBack(force);
            }
        }
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
        if (currentState == PlayerState.invulnerable || currentState == PlayerState.Dash)
        {
            return;
        }
        else
        {
            health -= damage;
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
            sr.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sr.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
    }

    private void OnDrawGizmos()
    {
        KongrooUtils.DrawGizmoCircle(transform.position, interactRange, Color.red);
    }
}
