using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mailSend : MonoBehaviour
{
    public GameObject targetImage;  // Ȱ��ȭ�� �̹��� ������Ʈ
    public Button activateButton;   // �̹����� Ȱ��ȭ�� ��ư

    private void Start()
    {
        activateButton.onClick.AddListener(ActivateImage);
    }

    private void ActivateImage()
    {
        targetImage.SetActive(true);
    }
}
