using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform content;  // Target Scroll View의 Content를 참조합니다.

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null && droppedObject.GetComponent<DragImage>() != null)
        {
            // 드래그된 객체를 새로 인스턴스화하여 추가
            GameObject newImage = Instantiate(droppedObject, content);
            newImage.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
