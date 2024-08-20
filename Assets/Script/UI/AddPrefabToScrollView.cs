using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddPrefabToScrollView : MonoBehaviour
{
    public TMP_InputField inputField;  // TMP_InputField 컴포넌트를 참조합니다.
    public Button button;              // Button 컴포넌트를 참조합니다.
    public GameObject prefab;          // Prefab을 참조합니다.
    public Transform content;          // Scroll View의 Content를 참조합니다.

    private void Start()
    {
        // 버튼 클릭 시 onClick 이벤트에 대한 리스너를 추가합니다.
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // InputField에 입력된 텍스트를 가져옵니다.
        string inputText = inputField.text;

        // Prefab을 인스턴스화하여 Scroll View의 Content 안에 추가합니다.
        GameObject newPrefab = Instantiate(prefab, content);

        // 인스턴스화한 Prefab 안의 TMP_Text 컴포넌트를 찾아 텍스트를 설정합니다.
        TMP_Text textComponent = newPrefab.GetComponentInChildren<TMP_Text>();

        if (textComponent != null)
        {
            textComponent.text = inputText;
        }
        else
        {
            Debug.LogWarning("Prefab 안에 TMP_Text 컴포넌트가 없습니다.");
        }
    }
}
