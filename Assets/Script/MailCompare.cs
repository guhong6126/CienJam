using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailCompare : MonoBehaviour
{
    public TMP_Text referenceText;       // ������ �Ǵ� TMP_Text
    public TMP_InputField inputField;    // ����ڰ� �Է��ϴ� TMP_InputField
    public Button compareButton;         // �� ��ư
    public GameObject targetImage;       // Ȱ��ȭ�� Image ������Ʈ

    private void Start()
    {
        compareButton.onClick.AddListener(CompareText);
    }

    private void CompareText()
    {
        if (referenceText.text == inputField.text)
        {
            Debug.Log("�ؽ�Ʈ ��ġ");
            
        }
        else
        {
            Debug.Log("�ؽ�Ʈ ����ġ");
        }
        ActivateTargetImage();
    }

    private void ActivateTargetImage()
    {
        targetImage.SetActive(true);
    }
}
