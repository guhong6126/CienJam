using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] dropables;
    [SerializeField] private TMP_Text nameText;
    public Transform parent;
    public EventSystem eventSystem;
    public GraphicRaycaster graphicRaycaster;
    public GameObject prefab_;
    public bool isDragging = false;
    private GameObject selectedObject = null;
    public bool isClickingBomb;
    private bool isOnce;
    private Vector3 offset;
    private Vector3 previousMousePosition;
    private Vector3 currentMousePosition;
    private Vector2 previousPos;
    [SerializeField] private List<int> bottoms;

    public GameObject detailPage;

    private Color color;
    private RectTransform rectTransform;
    
    public Transform parentObj;
    public GameObject prefab;

    public bool isInRange;
    
    public DropArea dropArea;

    public RightClickManager RightClickManager;
    public GameObject RightClickMenu;
    

    void Update()
    {
        currentMousePosition = Input.mousePosition;

        if (Input.GetMouseButtonDown(1))
        {
            Debug.LogError("Click");

            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = currentMousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEventData, raycastResults);

            bool isIcon = false;

            foreach (RaycastResult result in raycastResults)
            {
                Debug.Log(result.gameObject.name);

                if (result.gameObject.GetComponent<ObjController>() != null)
                {
                    Debug.LogError("DSDS");
                    isIcon = true;
                }
                else
                {
                    Debug.LogError("DASD");
                }
            }

            RightClickMenu.SetActive(true);
            RightClickMenu.transform.SetAsLastSibling();
            RightClickMenu.transform.position = new Vector2(currentMousePosition.x + 65f, currentMousePosition.y - 80f);
            RightClickManager.isIcon = isIcon;
        }

        
        
        if (Input.GetMouseButtonDown(0))
        {
            RightClickMenu.SetActive(false);
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = currentMousePosition; 
            List<RaycastResult> raycastResults = new List<RaycastResult>(); 
            graphicRaycaster.Raycast(pointerEventData, raycastResults);
            
            
            foreach (RaycastResult result in raycastResults)
            {
                if (result.gameObject.GetComponent<ObjController>() != null)
                {
                    if (result.gameObject.transform.parent.gameObject.GetComponentInChildren<DropArea>() != null)
                    {
                        dropArea = result.gameObject.transform.parent.gameObject.GetComponentInChildren<DropArea>();
                    }
                    
                    if (result.gameObject.GetComponent<ObjController>().objType.isParent)
                    {
                        
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
                    GameObject obj = Instantiate(prefab, dropArea.detectionArea.transform.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
                    obj.name = selectedObject.name;
                    obj.GetComponent<InstManager>().chageName();

                    //파일 전송 퀘스트 성공 여부 확인
                    List<string> nameIndex = new List<string>() { "이대리", "남대리", "나대리", "김과장", "이과장", "한과장", "차과장", "김부장", "임부장" };
                    int receiveIndex = nameIndex.IndexOf((string)dropArea.detectionArea.gameObject.transform.parent.gameObject.name) + 1;
                    QuestManager quest = GameObject.Find("GameManager").GetComponent<QuestManager>();
                    bool isExist = false;
                    foreach (KeyValuePair<int, QuestData> kv in quest.questList)
                    {
                        if (kv.Key == 10 + receiveIndex)
                        {
                            isExist = true;
                        }
                    }
                    if (isExist)
                    {
                        if (quest.questList[10 + receiveIndex].fileName == obj.name)
                        {
                            quest.SuccessQuest(10 + receiveIndex);
                        }
                    }
                    dropArea.targetUI = null;
                    isInRange = false;
                }
                selectedObject.gameObject.transform.position = previousPos;
                color.a = 1f;
                selectedObject.GetComponent<Image>().color = color;
                isClickingBomb = false;
                isOnce = false;
                
            }
            
            selectedObject = null;
        }
    }

    public void setBottom(int i)
    {
        if (!bottoms.Contains(i))
        {
            bottoms.Add(i);
            GameObject gm = Instantiate(prefab_, parent);
            gm.GetComponent<BottomListManager>().id = i;
            gm.GetComponent<BottomListManager>().setImgNText();
        }
    }

    public void closeBottom(int i)
    {
        if (bottoms.Contains(i))
        {
            bottoms.Remove(i);
            BottomListManager theobj = null;
            foreach (BottomListManager obj in parent.GetComponentsInChildren<BottomListManager>())
            {
                if (obj.id == i)
                {
                    theobj = obj;
                }
            }
            Destroy(theobj.gameObject);
        }
        
    }



}
