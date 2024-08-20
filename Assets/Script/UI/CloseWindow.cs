using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CloseWindow : MonoBehaviour, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // 클릭된 오브젝트를 부모의 가장 마지막 자식으로 이동시킴
        transform.SetAsLastSibling();
    }
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
