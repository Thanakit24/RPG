using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;
    public Transform[] playerSpawn;
    public Transform[] enemySpawn;
    public List<Player> Players { get; set; } = new List<Player>();
    [SerializeField] private Player playerPrefab;
    public List<BaseEnemy> Enemies { get; set; } = new List<BaseEnemy>();
    [SerializeField] private BaseEnemy enemyPrefab;

    private void Awake() => Instance = this;

    //private void Start()
    //{
    //    foreach (Transform spawn in playerSpawn)
    //    {
    //        for (int i = 0; i < 1; i++)
    //        {
    //            var unit = Instantiate(playerPrefab, spawn);
    //        }
    //    }
    //    foreach (Transform spawn in enemySpawn)
    //    {
    //        for (int i = 0; i < 1; i++)
    //        {
    //            var unit = Instantiate(enemyPrefab, spawn);
    //        }
    //    }
    //}
}