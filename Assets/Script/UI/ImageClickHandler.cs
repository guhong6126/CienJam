using UnityEngine;
using UnityEngine.UI;

public class ImageClickHandler : MonoBehaviour
{
    // 팝업창으로 사용할 패널 (기존에 있는 UI 패널)
    public GameObject popupPanel = null;

    public void OnImageClick()
    {
        // 팝업 패널의 활성화 상태를 토글 (켜져있으면 끄고, 꺼져있으면 켜기)
        bool isActive = popupPanel.activeSelf;
        popupPanel.SetActive(!isActive);
    }

}
