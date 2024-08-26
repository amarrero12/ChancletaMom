using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class switchScene : MonoBehaviour
{
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetStats()
    {
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.DeleteKey("Platanos");
    }

    void Update()
    {
        // if(SceneManager.GetActiveScene().buildIndex == 0)
        // {
        //     if(Input.GetButtonDown("Jump"))
        //     {
        //         ResetStats();
        //         LoadNextScene();
        //     }
        // }
        
    }

    void Start()
    {
        //GameObject.Find("LevelSelectScreen").GetComponent<Canvas>().enabled = false;
    }


    public void LevelSelectScreen()
    {
        GameObject.Find("LevelSelectScreen").GetComponent<Canvas>().enabled = true;

        GameObject.Find("MainScreen").GetComponent<Canvas>().enabled = false;
        //GameObject.Find("LevelSelectScreen").SetActive(true);
    }

    public void Back()
    {
        GameObject.Find("MainScreen").GetComponent<Canvas>().enabled = true;
        GameObject.Find("LevelSelectScreen").GetComponent<Canvas>().enabled = false;
    }

    public void LoadLvl1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLvl2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLvl3()
    {
        SceneManager.LoadScene(3);
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
