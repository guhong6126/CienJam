using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerManager : MonoBehaviour
{
    RectTransform rectTrans;

    private void Start()
    {
        rectTrans = GetComponent<RectTransform>();
    }

    public void OrderManage()
    {
        rectTrans.SetAsFirstSibling();
    }
}
