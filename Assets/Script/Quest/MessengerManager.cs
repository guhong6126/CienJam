using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerManager : MonoBehaviour
{
    [SerializeField] RectTransform rectTrans;

    public void OrderManage()
    {
        rectTrans.SetAsFirstSibling();
    }
}
