using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPopup : MonoBehaviour
{
    public TMP_InputField inputField;  // ����ڰ� Ŭ���� TMP_InputField
    public GameObject popupWindow;     // ������ â (�˾� â)

    private void Start()
    {
        // Input Field�� OnSelect �̺�Ʈ ������ �߰�
        inputField.onSelect.AddListener(OnInputFieldSelected);

        // �˾� â�� ��Ȱ��ȭ ���·� ����
        popupWindow.SetActive(false);
    }

    // Input Field�� ���õǾ��� �� ȣ��Ǵ� �޼���
    private void OnInputFieldSelected(string text)
    {
        // �˾� â�� Ȱ��ȭ
        popupWindow.SetActive(true);
    }
}
