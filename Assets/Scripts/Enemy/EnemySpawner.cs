using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    private List<Enemy> alive = new List<Enemy>();
    public bool playerCollided = false; 
    public GameObject tilemap;
    //public Animator tilemapAnimator;
    public float spawnInterval;
    public float startInterval;
    public float disableTime;
    public bool isTrap;
    public bool started = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrap)
        {
            if (enemies.Count == 0 && alive.Count == 0)
            {
                Invoke("Disable", disableTime);
            }
        }

        if (playerCollided)
        {
            startInterval -= Time.deltaTime;
            if (startInterval <= 0 && !started)
            {
                started = true;
                StartCoroutine(SpawnEnemies());
            }
        }
    }

    private void Disable()
    {
        tilemap.SetActive(false);
    }

    private IEnumerator SpawnEnemies()
    {
        //inside of a for loop u cant change the array therefore we use while //cant remove & add stuff to the array
        while (enemies.Count > 0)
        {  
            //Get and remove first enemy
            GameObject enemy = enemies[0];
            enemies.RemoveAt(0);

            //Add to alive enemies
            Enemy enemyS = enemy.GetComponent<Enemy>();
            alive.Add(enemyS);

            //Make enemy alive and sub to death
            enemy.SetActive(true);
            enemyS.deathEvent += EnemyDied;

            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

    private void EnemyDied(Enemy e)
    {
        e.deathEvent -= EnemyDied;
        //Destroy(e.gameObject);
        alive.Remove(e);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //print("Run coroutine");
            playerCollided = true;
            tilemap.SetActive(enabled);
            //tilemapAnimator.SetTrigger("Enabled");

        }
    }
}
