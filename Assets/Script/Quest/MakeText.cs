using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeText : MonoBehaviour
{
    [SerializeField] GameObject textUI;
    [SerializeField] GameObject messenger;
    public Transform trans;
    TMP_Text tmp;

    public void MakeT(string content, string var1, string var2)
    {
        GameObject gameObj = Instantiate(textUI,trans);
        tmp = gameObj.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        if (var2 != null)
        {
            tmp.text = string.Format(content, var1, var2);
        }
        else if (var1 != null)
        {
            tmp.text = string.Format(content, var1);
        }
        else
        {
            tmp.text = content;
        }

        messenger.transform.GetChild(1).gameObject.SetActive(true);
    }
}
