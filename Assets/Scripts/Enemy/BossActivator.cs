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

    // Start is called before the first frame update
    void Start()
    {
        boomerBoss.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossCam.activeSelf)
        {
            playerCam.SetActive(false);
            camCutscenetime -= Time.deltaTime;
            if (camCutscenetime <= 0)
            {
                bossCam.SetActive(false);
                playerCam.SetActive(true);
                boomerBoss.enabled = true;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bossCam.SetActive(true);
            trapColliders.SetActive(true);
            displayTilemap.SetActive(true);
        }

       
    }
}
