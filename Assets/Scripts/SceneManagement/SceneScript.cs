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
            //EnemySpawner[] enemySpawners = FindObjectsOfType<EnemySpawner>();
            GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("LoadOnce");
            for (int i = 0; i < objectsToDestroy.Length; i++)
            {
                Destroy(objectsToDestroy[i]);
            }
        }
        else
        {
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
        SaveSceneItems();
        SaveSystem.SavePlayer(GameManager.instance.player);
        transitionIndex = index;
        SceneManager.LoadScene(sceneToLoad);
    }
    public void RestartFromSave()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
