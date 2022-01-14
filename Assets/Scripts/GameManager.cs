using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController player;
    public TMP_Text currencyText;
    public TMP_Text interactText;
    public ItemPickup[] itemPickups;
    public List<SceneTransition> transitions;
    public GameObject deadUI;

    private void Awake()
    {
        Time.timeScale = 1f;
        instance = this;
    }

    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            //Start tutorial or go to home screen
        }
        else
        {
            data.PopulatePlayer(player);
            player.transform.position = transitions[SceneScript.instance.transitionIndex].spawnPoint.position;
        }
    }

    private void Update()
    {
        currencyText.text = player.invManager.currency.ToString();
    }

    public void PlayerDied()
    {
        deadUI.SetActive(true);
        Time.timeScale = 0f;
        Destroy(player);
    }
}