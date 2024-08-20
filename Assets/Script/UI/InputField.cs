using UnityEngine;
using TMPro;

public class InputField : MonoBehaviour
{
    public TMP_InputField inputField;  // TMP_InputField를 참조
    private string storedText;         // 입력된 텍스트를 저장할 변수

    void Start()
    {
        storedText = string.Empty;
    }

    public void SaveInputText()
    {
        storedText = inputField.text;
    }
}
