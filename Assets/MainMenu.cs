using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.Play("Theme");
    }
    public void Play()
    {
        SceneManager.LoadScene("Section 1");
    }

   public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
