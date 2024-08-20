using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler
{
    public Transform content;  // Target Scroll View�� Content�� �����մϴ�.

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            Debug.Log("Dropped Object: " + droppedObject.name); // ��� �̺�Ʈ �α�

            // �巡�׵� ��ü�� Content �ȿ� �߰��ϱ�
            droppedObject.transform.SetParent(content, false);

            // ����� ��ü�� ��ġ�� �ٽ� ����
            droppedObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}
