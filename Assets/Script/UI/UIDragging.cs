using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragging : MonoBehaviour
{
    private LayerMask uiLayerMask;
    private GameObject selectedObject;
    private GameObject ghostObject;  // ���Ӱ� ������ ������ ������Ʈ
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    private Canvas canvas;
    private Vector2 dragOffset;  // ������ ���� �߰�

    void Start()
    {
        uiLayerMask = LayerMask.GetMask("File");

        // GraphicRaycaster�� Canvas ��ü�� �־�� ��
        raycaster = GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster is missing from the GameObject.");
        }

        // EventSystem�� Scene�� �־�� ��
        eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            Debug.LogError("EventSystem is missing from the Scene.");
        }

        // Canvas ��ü�� ����� �����Ǿ����� Ȯ��
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas is missing from the parent GameObject.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SelectObject();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DestroyGhostObject();
            selectedObject = null;
        }

        if (ghostObject != null)
        {
            DragGhostObject();
        }
    }

    void SelectObject()
    {
        if (eventSystem == null || raycaster == null) return;

        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameObject hitObject = result.gameObject;
            if (((1 << hitObject.layer) & uiLayerMask) != 0 && hitObject.GetComponent<Image>() != null)
            {
                selectedObject = hitObject;
                CreateGhostObject();  // ������ ������Ʈ ����
                Debug.Log($"Selected object: {selectedObject.name}");
                break;
            }
        }
    }

    void CreateGhostObject()
    {
        // ���� ������Ʈ�� �����Ͽ� ������ ������Ʈ ����
        ghostObject = Instantiate(selectedObject, selectedObject.transform.parent);
        var image = ghostObject.GetComponent<Image>();
        var color = image.color;
        color.a = 0.5f;  // ���������� ����
        image.color = color;

        // ���콺 ��ġ�� ���� ������ ������Ʈ ��ġ ���� (������ ���)
        RectTransform ghostRectTransform = ghostObject.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        // �������� ����ϰ� ����
        RectTransform selectedRectTransform = selectedObject.GetComponent<RectTransform>();
        dragOffset = (Vector2)selectedRectTransform.localPosition - localPoint;
        ghostRectTransform.localPosition = localPoint + dragOffset;
    }

    void DragGhostObject()
    {
        RectTransform rectTransform = ghostObject.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        rectTransform.localPosition = localPoint + dragOffset;
    }

    void DestroyGhostObject()
    {
        // �巡�װ� ������ ������ ������Ʈ�� ����
        if (ghostObject != null)
        {
            Destroy(ghostObject);
            ghostObject = null;
        }
    }
}
