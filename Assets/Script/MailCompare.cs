using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailCompare : MonoBehaviour
{
    public TMP_Text referenceText;       // 기준이 되는 TMP_Text
    public TMP_InputField inputField;    // 사용자가 입력하는 TMP_InputField
    public Button compareButton;         // 비교 버튼
    public GameObject targetImage;       // 활성화할 Image 오브젝트

    private void Start()
    {
        compareButton.onClick.AddListener(CompareText);
    }

    private void CompareText()
    {
        if (referenceText.text == inputField.text)
        {
            Debug.Log("텍스트 일치");
            
        }
        else
        {
            Debug.Log("텍스트 불일치");
        }
        ActivateTargetImage();
    }

    private void ActivateTargetImage()
    {
        targetImage.SetActive(true);
    }
}
