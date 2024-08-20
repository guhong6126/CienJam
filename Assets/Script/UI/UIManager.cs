using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dropables;

    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;

    public bool isDragging = false;
    private GameObject selectedObject = null;
    public bool isClickingBomb;
    private bool isOnce;
    private Vector3 offset;
    private Vector3 previousMousePosition;
    private Vector3 currentMousePosition;
    private Vector2 previousPos;

    private Color color;
    private RectTransform rectTransform;
    
    DropArea dropArea;

    private void Awake()
    {
        dropArea = FindObjectOfType<DropArea>();
    }

    void Update()
    {
        currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = currentMousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.GetComponent<ObjController>() != null)
                {
                    if (result.gameObject.GetComponent<ObjController>().objType.isParent)
                    {
                        Debug.Log(result.gameObject.name);
                        selectedObject = result.gameObject.transform.parent.gameObject;
                        isDragging = true;
                    }
                    else if (result.gameObject.GetComponent<ObjController>().objType.isNeedtoReorder)
                    {
                        selectedObject = result.gameObject;
                        dropArea.targetUI = result.gameObject.GetComponent<RectTransform>();
                        if (!isOnce)
                        {
                            previousPos = selectedObject.transform.position;
                            isOnce = true;
                        }

                        if (selectedObject.GetComponent<Image>() != null)
                        {
                            color = selectedObject.GetComponent<Image>().color;
                            color.a = 0.6f;
                            selectedObject.GetComponent<Image>().color = color;
                        }
                        isDragging = true;
                        isClickingBomb = true;
                    }
                    else
                    {
                        selectedObject = result.gameObject;
                        isDragging = true;
                    }

                    RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, currentMousePosition, Camera.main, out var worldPos);
                        offset = rectTransform.position - worldPos;
                    }

                    previousMousePosition = currentMousePosition;
                }
            }
        }

        if (isDragging && selectedObject != null)
        {
            RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                Vector3 worldPos;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, currentMousePosition, Camera.main, out worldPos);

                Vector3 delta = (currentMousePosition - previousMousePosition);
                rectTransform.position += delta;

                previousMousePosition = currentMousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isDragging && isClickingBomb)
            {
                selectedObject.gameObject.transform.position = previousPos;
                color.a = 1f;
                selectedObject.GetComponent<Image>().color = color;
                dropArea.targetUI = null;
                isClickingBomb = false;
                isOnce = false;
            }
            isDragging = false;
            selectedObject = null;
        }
    }

}
