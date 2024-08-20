using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddPrefabToScrollView : MonoBehaviour
{
    public TMP_InputField inputField;  // TMP_InputField ������Ʈ�� �����մϴ�.
    public Button button;              // Button ������Ʈ�� �����մϴ�.
    public GameObject prefab;          // Prefab�� �����մϴ�.
    public Transform content;          // Scroll View�� Content�� �����մϴ�.

    private void Start()
    {
        // ��ư Ŭ�� �� onClick �̺�Ʈ�� ���� �����ʸ� �߰��մϴ�.
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // InputField�� �Էµ� �ؽ�Ʈ�� �����ɴϴ�.
        string inputText = inputField.text;

        // Prefab�� �ν��Ͻ�ȭ�Ͽ� Scroll View�� Content �ȿ� �߰��մϴ�.
        GameObject newPrefab = Instantiate(prefab, content);

        // �ν��Ͻ�ȭ�� Prefab ���� TMP_Text ������Ʈ�� ã�� �ؽ�Ʈ�� �����մϴ�.
        TMP_Text textComponent = newPrefab.GetComponentInChildren<TMP_Text>();

        if (textComponent != null)
        {
            textComponent.text = inputText;
        }
        else
        {
            Debug.LogWarning("Prefab �ȿ� TMP_Text ������Ʈ�� �����ϴ�.");
        }
    }
}
