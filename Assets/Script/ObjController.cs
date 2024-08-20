using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjController : MonoBehaviour
{
    public ObjType objType;
    public TMP_Text text;

    private void Start()
    {
        if (objType.isNeedtoReorder)
        {
            gameObject.name = text.text;
        }
    }
}
