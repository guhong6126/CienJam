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

    // ����� �� �ִ� ������ ���� �� ���� �����ϵ��� ����
    public List<GameObject> dropAreas;

    void Start()
    {
        uiLayerMask = LayerMask.GetMask("File");

        raycaster = GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster is missing from the GameObject.");
        }

        eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            Debug.LogError("EventSystem is missing from the Scene.");
        }

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
            if (ghostObject != null)
            {
                TryDropObject();
                DestroyGhostObject();
            }
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
        ghostObject = Instantiate(selectedObject, selectedObject.transform.parent);
        var image = ghostObject.GetComponent<Image>();
        var color = image.color;
        color.a = 0.5f;  // ���������� ����
        image.color = color;

        // ghostObject�� ���̾ "Ignore Raycast"�� �����Ͽ� Raycast���� ����
        ghostObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        RectTransform ghostRectTransform = ghostObject.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

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
        if (ghostObject != null)
        {
            ghostObject.layer = LayerMask.NameToLayer("UI");  // ���� ���̾�� �ǵ�����
            Destroy(ghostObject);
            ghostObject = null;
        }
    }

    void TryDropObject()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerEventData, results);

        // Ignore Raycast ���̾ �����ϴ� ���͸�
        results.RemoveAll(result => result.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"));

        foreach (RaycastResult result in results)
        {
            GameObject hitObject = result.gameObject;
            Debug.Log($"Raycast hit: {hitObject.name} on layer {LayerMask.LayerToName(hitObject.layer)}");

            foreach (var dropArea in dropAreas)
            {
                ScrollRect scrollRect = dropArea.GetComponent<ScrollRect>();
                if (scrollRect != null)
                {
                    GameObject viewport = scrollRect.viewport.gameObject;

                    // Viewport ������ ��ӵǾ����� Ȯ��
                    if (hitObject == viewport || hitObject.transform.IsChildOf(viewport.transform))
                    {
                        Debug.Log($"Dropped on viewport: {viewport.name}");
                        CreateDroppedImage(scrollRect.content.gameObject);
                        return;
                    }
                }
            }
        }

        Debug.Log("No valid drop area found.");
    }

    void CreateDroppedImage(GameObject parent)
    {
        GameObject newImage = Instantiate(selectedObject, parent.transform);

        // �θ��� �߾ӿ� ��ġ��Ű�ų� �ʿ��� ��ġ�� ����
        RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
        newImageRectTransform.localPosition = Vector3.zero; // �� ��ġ�� Content ���� ������ �������� Ȯ��
        newImageRectTransform.sizeDelta = new Vector2(100, 100); // �ʿ信 ���� ũ�⸦ ����

        var image = newImage.GetComponent<Image>();
        var color = image.color;
        color.a = 1.0f;  // �������ϰ� ����
        image.color = color;

        Debug.Log($"Dropped object into {parent.name}");
    }
}
