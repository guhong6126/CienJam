using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void start()
    {
        SceneManager.LoadScene("Round 1");
    }

    public void calculate()
    {
        SceneManager.LoadScene("Start");
    }
}
