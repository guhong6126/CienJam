using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowPopup : MonoBehaviour
{
    public TMP_InputField inputField;  // 사용자가 클릭할 TMP_InputField
    public GameObject popupWindow;     // 임의의 창 (팝업 창)

    private void Start()
    {
        // Input Field에 OnSelect 이벤트 리스너 추가
        inputField.onSelect.AddListener(OnInputFieldSelected);

        // 팝업 창을 비활성화 상태로 시작
        popupWindow.SetActive(false);
    }

    // Input Field가 선택되었을 때 호출되는 메서드
    private void OnInputFieldSelected(string text)
    {
        // 팝업 창을 활성화
        popupWindow.SetActive(true);
    }
}
