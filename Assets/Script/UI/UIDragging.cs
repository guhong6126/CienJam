using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragging : MonoBehaviour
{
    private LayerMask uiLayerMask;
    private GameObject selectedObject;
    private GameObject ghostObject;  // 새롭게 생성될 반투명 오브젝트
    private GraphicRaycaster raycaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;
    private Canvas canvas;
    private Vector2 dragOffset;  // 오프셋 변수 추가

    // 드롭할 수 있는 영역을 여러 개 설정 가능하도록 변경
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
                CreateGhostObject();  // 반투명 오브젝트 생성
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
        color.a = 0.5f;  // 반투명으로 설정
        image.color = color;

        // ghostObject의 레이어를 "Ignore Raycast"로 설정하여 Raycast에서 제외
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
            ghostObject.layer = LayerMask.NameToLayer("UI");  // 원래 레이어로 되돌리기
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

        // Ignore Raycast 레이어를 무시하는 필터링
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

                    // Viewport 영역에 드롭되었는지 확인
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

        // 부모의 중앙에 위치시키거나 필요한 위치로 조정
        RectTransform newImageRectTransform = newImage.GetComponent<RectTransform>();
        newImageRectTransform.localPosition = Vector3.zero; // 이 위치가 Content 영역 내에서 적절한지 확인
        newImageRectTransform.sizeDelta = new Vector2(100, 100); // 필요에 따라 크기를 조정

        var image = newImage.GetComponent<Image>();
        var color = image.color;
        color.a = 1.0f;  // 불투명하게 설정
        image.color = color;

        Debug.Log($"Dropped object into {parent.name}");
    }
}
