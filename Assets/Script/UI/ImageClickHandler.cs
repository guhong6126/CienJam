using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{

    public GameObject popupPanel = null;

    public void OnImageClick()
    {
        popupPanel.transform.SetAsLastSibling();
        popupPanel.SetActive(true);
    }

}
