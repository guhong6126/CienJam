using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailCompare : MonoBehaviour
{
    public TMP_Text referenceText;       // 기준이 되는 TMP_Text
    public TMP_InputField inputField;    // 사용자가 입력하는 TMP_InputField
    public Button compareButton;         // 비교 버튼

    private void Start()
    {
        // 버튼 클릭 시 onClick 이벤트에 대한 리스너를 추가합니다.
        compareButton.onClick.AddListener(CompareText);
    }

    private void CompareText()
    {
        // 두 텍스트가 같은지 비교
        if (referenceText.text == inputField.text)
        {
            Debug.Log("텍스트 일치");
        }
        else
        {
            Debug.Log("텍스트 불일치");
        }
    }
}
