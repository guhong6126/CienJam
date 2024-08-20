using UnityEngine;
using TMPro;

public class InputField : MonoBehaviour
{
    public TMP_InputField inputField;  // TMP_InputField�� ����
    public string storedText;         // �Էµ� �ؽ�Ʈ�� ������ ����

    void Start()
    {
        storedText = string.Empty;
    }

    public void SaveInputText()
    {
        storedText = inputField.text;
    }
}
