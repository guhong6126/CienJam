using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CloseWindow : MonoBehaviour, IPointerClickHandler
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 오브젝트를 부모의 가장 마지막 자식으로 이동시킴
        transform.SetAsLastSibling();
    }
    

    public void Show()
    {
        gameObject.SetActive(true);
        audioManager.clickAudio.Play();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        audioManager.clickAudio.Play();
    }
}
