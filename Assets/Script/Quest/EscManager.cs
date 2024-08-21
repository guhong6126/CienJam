using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscManager : MonoBehaviour
{
    public List<GameObject> gameObj = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObj != null)
            {
                gameObj[0].SetActive(false);
                gameObj.RemoveAt(0);
            }
        }
    }
}
