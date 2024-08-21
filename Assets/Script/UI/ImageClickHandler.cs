using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{

    public GameObject popupPanel = null;
    EscManager esc;

    private void Start()
    {
        esc = GameObject.Find("GameManager").GetComponent<EscManager>();
    }

    public void OnImageClick()
    {
        popupPanel.transform.SetAsLastSibling();
        popupPanel.SetActive(true);
        if (esc.gameObj.Contains(popupPanel))
        {
            esc.gameObj.Remove(popupPanel);
            esc.gameObj.Insert(0, popupPanel);
        }
        else
        {
            esc.gameObj.Insert(0, popupPanel);
        }
    }

}
