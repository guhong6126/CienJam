using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InstManager : MonoBehaviour
{
    public TMP_Text text;

    public void chageName()
    {
        text.text = gameObject.name;
    }
}
