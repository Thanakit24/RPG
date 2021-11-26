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

    private void Awake()
    {
        Time.timeScale = 1f;
        instance = this;
        PlayerData data = SaveSystem.LoadPlayer();
        data.PopulatePlayer(player);
        player.transform.position = transitions[SceneScript.instance.transitionIndex].spawnPoint.position;
       
    }
    private void Update()
    {
        currencyText.text = player.invManager.currency.ToString();
    }
}
