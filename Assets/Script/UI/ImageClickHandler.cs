using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{

    public GameObject popupPanel = null;

    public void OnImageClick()
    {
        bool isActive = popupPanel.activeSelf;
        popupPanel.SetActive(!isActive);
    }

}
