using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dropables;
    [SerializeField] private TMP_Text nameText;

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

    public GameObject detailPage;

    private Color color;
    private RectTransform rectTransform;
    
    public Transform parentObj;
    public GameObject prefab;

    public bool isInRange;
    
    public DropArea dropArea;

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
                        nameText.text = "File Name: " + result.gameObject.name;
                        if(dropArea != null)
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
            isDragging = false; 
            if (!isDragging && isClickingBomb)
            {
                if (isInRange)
                {
                    Debug.LogError("Target UI is Instantiated");
                    GameObject obj = Instantiate(prefab, dropArea.detectionArea.transform.gameObject.GetComponentInChildren<GameObject>().transform);
                    obj.name = selectedObject.name;
                    obj.GetComponent<InstManager>().chageName();
                    Debug.Log(obj.name);
                    dropArea.targetUI = null;
                    isInRange = false;
                }
                selectedObject.gameObject.transform.position = previousPos;
                color.a = 1f;
                selectedObject.GetComponent<Image>().color = color;
                isClickingBomb = false;
                isOnce = false;
                dropArea.targetUI = null;
            }
            
            selectedObject = null;
        }
    }



}
