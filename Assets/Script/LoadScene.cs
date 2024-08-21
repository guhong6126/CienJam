using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public GameObject[] turnoffObj;
    public GameObject TotalScreen;
    public scoreManager ScoreManager;
    
    public void start()
    {
        SceneManager.LoadScene("Round");
    }

    public void calculate()
    {
        SceneManager.LoadScene("Start");
    }


    public void quitGame()
    {
        ScoreManager.gameObject.SetActive(true);
        for (int i = 0; i < turnoffObj.Length; i++)
        {
            turnoffObj[i].SetActive(false);
        }
        
        ScoreManager.setScores();
    }

    public void quitApp()
    {
        Application.Quit();
    }
}


