using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailCompare : MonoBehaviour
{
    public TMP_Text referenceText;       // ������ �Ǵ� TMP_Text
    public TMP_InputField inputField;    // ����ڰ� �Է��ϴ� TMP_InputField
    public Button compareButton;         // �� ��ư

    private void Start()
    {
        // ��ư Ŭ�� �� onClick �̺�Ʈ�� ���� �����ʸ� �߰��մϴ�.
        compareButton.onClick.AddListener(CompareText);
    }

    private void CompareText()
    {
        // �� �ؽ�Ʈ�� ������ ��
        if (referenceText.text == inputField.text)
        {
            Debug.Log("�ؽ�Ʈ ��ġ");
        }
        else
        {
            Debug.Log("�ؽ�Ʈ ����ġ");
        }
    }
}
