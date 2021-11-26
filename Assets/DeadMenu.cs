using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadMenu : MonoBehaviour
{
   

  
    //public GameObject controlsUI;

    void Start()
    {
        
    }

    public void RetryFromSave()
    {
        //pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        //GameIsPaused = false;
        SceneScript.instance.RestartFromSave();
    }

    //public void Close()
    //{
    //    controlsUI.SetActive(false);
    //}

    //public void Controls()
    //{
    //    controlsUI.SetActive(true);
    //}

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
