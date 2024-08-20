using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mailSend : MonoBehaviour
{
    public GameObject targetImage;  // 활성화할 이미지 오브젝트
    public Button activateButton;   // 이미지를 활성화할 버튼

    private void Start()
    {
        activateButton.onClick.AddListener(ActivateImage);
    }

    private void ActivateImage()
    {
        targetImage.SetActive(true);
    }
}
