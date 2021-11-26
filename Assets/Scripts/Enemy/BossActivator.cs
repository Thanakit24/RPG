using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class BossActivator : MonoBehaviour
{
    public GameObject trapColliders;
    public GameObject displayTilemap;
    public BoomerBoss boomerBoss;
    public GameObject playerCam;
    public GameObject bossCam;
    
    public float camCutscenetime = 2.5f;
    private bool isBossActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        boomerBoss.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBossActivated && bossCam.activeSelf)
        {
            camCutscenetime -= Time.deltaTime;
            if (camCutscenetime <= 0)
            {
                print("boss start");
                bossCam.SetActive(false);
                playerCam.SetActive(true);
                boomerBoss.enabled = true;
                boomerBoss.deathEvent += BossDead;
                isBossActivated = true;
            }
        }
    }

    void BossDead(Enemy e)
    {
        //print("boss died, turning off trap");
        displayTilemap.SetActive(false);
        trapColliders.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBossActivated) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Player enter");
            bossCam.SetActive(true);
            playerCam.SetActive(false);
            trapColliders.SetActive(true);
            displayTilemap.SetActive(true);
        }
       
    }
}
