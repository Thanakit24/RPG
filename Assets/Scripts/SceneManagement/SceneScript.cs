using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    public static SceneScript instance;
    public List<int> loadedScenes = new List<int>();
    public List<int>[] sceneItems;
    public int transitionIndex =0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            InitScene();

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void InitScene()
    {
        sceneItems = new List<int>[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < sceneItems.Length; i++)
        {
            sceneItems[i] = new List<int>();
        }

        
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
        LoadSceneItems(scene.buildIndex);
    }

    public void SaveSceneItems()
    {
        for (int i = 0; i < GameManager.instance.itemPickups.Length; i++)
        {
            if (GameManager.instance.itemPickups[i] == null)
            {
                sceneItems[SceneManager.GetActiveScene().buildIndex].Add(i);
            }
        }
    }

    public void LoadSceneItems(int currentScene)
    {
        foreach (var itemIndex in sceneItems[currentScene])
        {
            ItemPickup item = GameManager.instance.itemPickups[itemIndex];
            Destroy(item.gameObject);
        }
    }

    public void LoadScene(string sceneToLoad, int index)
    {
        transitionIndex = index;
        SceneManager.LoadScene(sceneToLoad);
    }
}
