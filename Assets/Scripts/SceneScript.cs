using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript instance;
    public List<int> loadedScenes = new List<int>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (loadedScenes.Contains(scene.buildIndex))
        {
            EnemySpawner[] enemySpawners = FindObjectsOfType<EnemySpawner>();
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                Destroy(enemySpawners[i].gameObject);
            }
        }
        else
        {
            print("Visited ");
            loadedScenes.Add(scene.buildIndex);
        }
    }

    private void Start()
    {
        
    }

}
