using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform content;  // Target Scroll View의 Content를 참조합니다.

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log("Dropped Object: " + droppedObject.name); // 드롭 이벤트 로그

            // 드래그된 객체를 Content 안에 추가하기
            droppedObject.transform.SetParent(content, false);

            // 드롭한 객체의 위치를 다시 설정
            droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}
