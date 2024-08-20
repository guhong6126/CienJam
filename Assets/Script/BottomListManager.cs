using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BottomListManager : MonoBehaviour
{
    public Sprite[] sprites;
    public int id;
    [SerializeField] private Image imgs;
    public TMP_Text text;

    public void setImgNText()
    {
        imgs.sprite = sprites[id];
        switch (id)
        {
            case 0:
                text.text = "파일";
                break;
            case 1:
                text.text = "메신저";
                break;
            case 2:
                text.text = "메일";
                break;
        }
    }
}
