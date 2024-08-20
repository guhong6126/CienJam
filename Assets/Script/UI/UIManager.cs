using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dropables;
    
    
    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;
    
    private bool isDragging = false;
    private GameObject selectedObject = null;
    private Vector3 offset;
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);
            
            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.GetComponent<ObjController>() != null)
                {
                    if (result.gameObject.name == "Top Bar")
                    {
                        Debug.Log(result.gameObject.name);
                        selectedObject = result.gameObject.transform.parent.gameObject;
                        isDragging = true;
                    }
                    else
                    {
                        selectedObject = result.gameObject;
                        isDragging = true;
                    }

                    
                    
                    RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out var worldPos);
                        offset = rectTransform.position - worldPos;
                    }
                }
            }
            
        }
        
        if (isDragging && selectedObject != null)
        {
            RectTransform rectTransform = selectedObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out var worldPos);
                rectTransform.position = worldPos + offset;
            }
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            selectedObject = null;
        }
    }
}
