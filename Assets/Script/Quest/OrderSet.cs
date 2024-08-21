using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSet : MonoBehaviour
{
    [SerializeField] GameObject gameObj;

    public void Order()
    {
        gameObj.transform.SetAsLastSibling();
    }
}
