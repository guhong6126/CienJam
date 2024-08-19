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

    public GameObject InputTextFalse = null;
    public GameObject InputTextTrue = null;

    // 다른 버튼에 연결할 메서드
    public void ImageFalse()
    {
        InputTextFalse.SetActive(false);
    }

    public void ImageTrue()
    {
        InputTextTrue.SetActive(true);
    }

    // Scroll View의 Content 오브젝트
    public Transform content = null;
    // 추가할 이미지의 프리팹
    public GameObject imagePrefab = null;

    public void AcceptQuest()
    {
        // Content 안에 이미지 프리팹을 생성
        GameObject newImage = Instantiate(imagePrefab, content);

        // 필요에 따라 위치나 크기 조정 가능
        // newImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
    }
}
