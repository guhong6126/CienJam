using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform content;  // Target Scroll View의 Content를 참조합니다.

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        Debug.Log("Dropped Object: " + droppedObject.name); // 로그 확인

        if (droppedObject != null && droppedObject.GetComponent<DragImage>() != null)
        {
            Debug.Log("Dropping into content: " + content.name); // 로그 확인

            // 드래그된 객체를 Content 안에 추가하기
            droppedObject.transform.SetParent(content, false);

            // 드롭한 객체의 위치를 다시 설정
            droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}
