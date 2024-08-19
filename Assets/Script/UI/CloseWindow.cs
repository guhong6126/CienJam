using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CloseWindow : MonoBehaviour
{
    void Start()
    {
        DOTween.Init();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        var seq = DOTween.Sequence();

        seq.Play();
    }

    public void Hide()
    {
        var seq = DOTween.Sequence();

        seq.Play().OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
