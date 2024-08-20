using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeText : MonoBehaviour
{
    [SerializeField] GameObject textUI;
    public Transform trans;
    TMP_Text tmp;

    public void MakeT(string content)
    {
        Debug.Log("1");
        GameObject gameObj = Instantiate(textUI,trans);
        tmp = gameObj.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TMP_Text>();
        tmp.text = content;
    }
}
