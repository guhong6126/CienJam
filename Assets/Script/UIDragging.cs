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

    void Start()
    {
        uiLayerMask = LayerMask.GetMask("File");

        // GraphicRaycaster는 Canvas 객체에 있어야 함
        raycaster = GetComponent<GraphicRaycaster>();
        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster is missing from the GameObject.");
        }

        // EventSystem은 Scene에 있어야 함
        eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            Debug.LogError("EventSystem is missing from the Scene.");
        }

        // Canvas 객체가 제대로 설정되었는지 확인
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
                CreateGhostObject();  // 반투명 오브젝트 생성
                Debug.Log($"Selected object: {selectedObject.name}");
                break;
            }
        }
    }

    void CreateGhostObject()
    {
        // 원래 오브젝트를 복제하여 반투명 오브젝트 생성
        ghostObject = Instantiate(selectedObject, selectedObject.transform.parent);
        var image = ghostObject.GetComponent<Image>();
        var color = image.color;
        color.a = 0.5f;  // 반투명으로 설정
        image.color = color;

        // 마우스 위치에 맞춰 반투명 오브젝트 위치 설정 (오프셋 고려)
        RectTransform ghostRectTransform = ghostObject.GetComponent<RectTransform>();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        // 오프셋을 계산하고 적용
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
        // 드래그가 끝나면 반투명 오브젝트를 제거
        if (ghostObject != null)
        {
            Destroy(ghostObject);
            ghostObject = null;
        }
    }
}
