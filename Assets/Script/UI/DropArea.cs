using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropArea : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public RectTransform targetUI; 
    public RectTransform detectionArea;
    

    public bool inRange;
    
    UIManager uIManager;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    void Update()
    {
        if (targetUI != null)
        {
            if (IsInRange(targetUI, detectionArea))
            {
                Debug.Log("Target UI is inside the detection range.");
                uIManager.isInRange = true;
            }
            else
            {
                Debug.Log("Target UI is outside the detection range.");
            }

        }
    }

    bool IsInRange(RectTransform target, RectTransform area)
    {
        Vector3[] targetCorners = new Vector3[4];
        Vector3[] areaCorners = new Vector3[4];
        
        target.GetWorldCorners(targetCorners);
        area.GetWorldCorners(areaCorners);
        
        if (targetCorners[0].x < areaCorners[2].x && targetCorners[2].x > areaCorners[0].x &&
            targetCorners[0].y < areaCorners[2].y && targetCorners[2].y > areaCorners[0].y)
        {
            return true;
        }

        return false;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        uIManager.dropArea = this;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        uIManager.dropArea = null;
    }
}