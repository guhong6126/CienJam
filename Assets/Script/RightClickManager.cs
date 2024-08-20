using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightClickManager : MonoBehaviour
{
    public bool isIcon;

    public GameObject[] first;
    public GameObject[] second;

    private void Update()
    {
        if (isIcon)
        {
            foreach (GameObject obj in first)
            {
                obj.GetComponent<Button>().interactable = true;
            }

            foreach (GameObject obj in second)
            {
                obj.GetComponent<Button>().interactable = false;
            }
        }
        else
        {
            if (!isIcon)
            {
                foreach (GameObject obj in first)
                {
                    obj.GetComponent<Button>().interactable = false;
                }

                foreach (GameObject obj in second)
                {
                    obj.GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}
