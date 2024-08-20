using System;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public RectTransform targetUI; 
    public RectTransform detectionArea;
    public Transform parentObj;

    
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
                if (Input.GetMouseButtonUp(0))
                {
                    uIManager.isDragging = false;
                    uIManager. isClickingBomb = false;
                    Destroy(targetUI.GetComponent<ObjController>());
                    targetUI.gameObject.transform.parent = parentObj;
                    targetUI = null;
                }
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

        // UI 오브젝트들의 코너 좌표 가져오기
        target.GetWorldCorners(targetCorners);
        area.GetWorldCorners(areaCorners);

        // 감지 범위와 대상 UI 오브젝트의 충돌 여부를 계산
        if (targetCorners[0].x < areaCorners[2].x && targetCorners[2].x > areaCorners[0].x &&
            targetCorners[0].y < areaCorners[2].y && targetCorners[2].y > areaCorners[0].y)
        {
            return true;
        }

        return false;
    }
}