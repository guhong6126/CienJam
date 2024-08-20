using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform content;  // Target Scroll View�� Content�� �����մϴ�.

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null && droppedObject.GetComponent<DragImage>() != null)
        {
            // �巡�׵� ��ü�� ���� �ν��Ͻ�ȭ�Ͽ� �߰�
            GameObject newImage = Instantiate(droppedObject, content);
            newImage.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }
}
